using System.Text.Json;
using AutoMapper;
using Business.Constants.Messages.Services.Authentication;
using Business.Services.Authentication.Abstract;
using Business.Services.Communication.Abstract;
using Core.Constants.StringConstants;
using Core.ExceptionHandling;
using Core.Security.SessionManagement;
using Core.Services.Messages;
using Core.Services.Payload;
using Core.Services.Result;
using Core.Utils.Auth;
using Core.Utils.Hashing;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Membership;
using Domain.DTOs.Authentication;
using Domain.DTOs.Membership;
using Domain.Entities.Membership;

namespace Business.Services.Authentication.Concrete;

public class AuthManager : IAuthService
{
    private readonly IBusinessDal _businessDal = ServiceTool.GetService<IBusinessDal>()!;
    private readonly ICustomerDal _customerDal = ServiceTool.GetService<ICustomerDal>()!;
    private readonly IMailingService _emailService = ServiceTool.GetService<IMailingService>()!;
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly ITokenHandler _tokenHandler = ServiceTool.GetService<ITokenHandler>()!;
    private readonly IUserDal _userDal = ServiceTool.GetService<IUserDal>()!;

    public async Task<ServiceObjectResult<LoginResponseDto?>> LoginUser(UserLoginDto userLoginDto)
    {
        var result = new ServiceObjectResult<LoginResponseDto?>();

        try
        {
            if (userLoginDto.Email != null)
                BusinessRules.Run(
                    ("AUTH-584337", BusinessRules.CheckEmail(userLoginDto.Email)));

            var user = await _userDal.GetAsync(
                u => u.Email == userLoginDto.Email || u.Username == userLoginDto.Username);

            if (user == null)
            {
                result.Fail(new ErrorMessage("AUTH-809431", AuthServiceMessages.NotFound));
                return result;
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                result.Fail(new ErrorMessage("AUTH-290694", AuthServiceMessages.WrongPassword));
                return result;
            }

            if (user.UseMultiFactorAuthentication)
            {
                user.LoginVerificationCode = GenerateMfaCode();
                user.LoginVerificationCodeExpiration = DateTime.Now.AddMinutes(1);
                user.LastLoginTime = DateTime.Now;
                await _userDal.UpdateAsync(user);
                var emailMessage =
                    $"Please use this code to login: {user.LoginVerificationCode}. The code will expire in 1 minute.";
                var mailResult = _emailService.SendSmtp(user.Email, "Verify Login", emailMessage);

                if (mailResult.HasFailed)
                {
                    var errDescription = mailResult.Messages[0].Description;
                    result.Fail(new ErrorMessage(mailResult.ResultCode!,
                        errDescription ?? AuthServiceMessages.VerificationCodeMailNotSent));

                    return result;
                }

                result.ExtraData.Add(new ServicePayloadItem("useMFA", true));
                result.Warning(AuthServiceMessages.MfaRequired);
                return result;
            }

            var token = _tokenHandler.GenerateToken(user.Id, user.Username, user.Email, user.Role, false);

            var serializedToken = JsonSerializer.Serialize(token);
            user.ActiveToken = serializedToken;
            await _userDal.UpdateAsync(user);

            var userGetDto = _mapper.Map<UserGetDto>(user);
            result.SetData(new LoginResponseDto { Token = token!, User = userGetDto },
                AuthServiceMessages.LoginSuccessful);
        }
        catch (ValidationException ex)
        {
            result.Fail(new ErrorMessage(ex.ExceptionCode, ex.Message));
        }
        catch (Exception ex)
        {
            result.Fail(new ErrorMessage("AUTH-347466", ex.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<bool>> LogoutUser(Guid userId)
    {
        var result = new ServiceObjectResult<bool>();
        try
        {
            var isGlobalAdmin = AuthHelper.IsLoggedInAsAdmin();

            if (!isGlobalAdmin)
                BusinessRules.Run(("AUTH-265570", BusinessRules.CheckIdSameWithCurrentUser(userId)));

            var user = await _userDal.GetAsync(p => p.Id.Equals(userId));
            BusinessRules.Run(("AUTH-202349", BusinessRules.CheckEntityNull(user)));

            user!.ActiveToken = null;
            await _userDal.UpdateAsync(user);

            result.SetData(true, AuthServiceMessages.LogoutSuccessful);
        }
        catch (ValidationException ex)
        {
            result.Fail(new ErrorMessage(ex.ExceptionCode, ex.Message));
        }
        catch (Exception ex)
        {
            result.Fail(new ErrorMessage("AUTH-800477", ex.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<bool>> RegisterBusiness(BusinessRegisterDto businessRegisterDto)
    {
        var result = new ServiceObjectResult<bool>();

        try
        {
            BusinessRules.Run(
                ("AUTH-906899", BusinessRules.CheckEmail(businessRegisterDto.Email)),
                ("AUTH-712957", await CheckIfEmailRegisteredBefore(businessRegisterDto.Email)),
                ("AUTH-770711", await CheckIfUsernameRegisteredBefore(businessRegisterDto.Username))
            );

            HashingHelper.CreatePasswordHash(businessRegisterDto.Password, out var passwordHash,
                out var passwordSalt);

            var user = _mapper.Map<Domain.Entities.Membership.Business>(businessRegisterDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = UserRoles.Business;

            await _businessDal.AddAsync(user);
            result.SetData(true, AuthServiceMessages.RegisterSuccessful);
        }
        catch (ValidationException ex)
        {
            result.Fail(new ErrorMessage(ex.ExceptionCode, ex.Message));
        }
        catch (Exception ex)
        {
            result.Fail(new ErrorMessage("AUTH-976141", ex.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<bool>> RegisterCustomer(CustomerRegisterDto customerRegisterDto)
    {
        var result = new ServiceObjectResult<bool>();

        try
        {
            BusinessRules.Run(
                ("AUTH-758067", BusinessRules.CheckEmail(customerRegisterDto.Email)),
                ("AUTH-824600", await CheckIfEmailRegisteredBefore(customerRegisterDto.Email)),
                ("AUTH-603241", await CheckIfUsernameRegisteredBefore(customerRegisterDto.Username))
            );

            HashingHelper.CreatePasswordHash(customerRegisterDto.Password, out var passwordHash,
                out var passwordSalt);

            var user = _mapper.Map<Customer>(customerRegisterDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = UserRoles.Customer;

            await _customerDal.AddAsync(user);
            result.SetData(true, AuthServiceMessages.RegisterSuccessful);
        }
        catch (ValidationException ex)
        {
            result.Fail(new ErrorMessage(ex.ExceptionCode, ex.Message));
        }
        catch (Exception ex)
        {
            result.Fail(new ErrorMessage("AUTH-855608", ex.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<LoginResponseDto?>> VerifyEmailCode(VerifyEmailCodeDto verifyCodeDto)
    {
        var result = new ServiceObjectResult<LoginResponseDto?>();
        try
        {
            var user = await _userDal.GetAsync(p => p.Email == verifyCodeDto.Email);
            if (user == null)
            {
                result.Fail(new ErrorMessage("AUTH-808079", AuthServiceMessages.NotFound));
                return result;
            }

            if (user.LoginVerificationCode != verifyCodeDto.Code)
            {
                result.Fail(new ErrorMessage("AUTH-755666", AuthServiceMessages.WrongVerificationCode));
                return result;
            }

            if (user.LoginVerificationCodeExpiration < DateTime.UtcNow)
            {
                result.Fail(new ErrorMessage("AUTH-221332", AuthServiceMessages.VerificationCodeExpired));
                return result;
            }

            user.LoginVerificationCode = null;
            user.LoginVerificationCodeExpiration = null;

            await _userDal.UpdateAsync(user);

            var token = _tokenHandler.GenerateToken(user.Id, user.Username, user.Email, user.Role, false);

            var userGetDto = _mapper.Map<UserGetDto>(user);
            result.SetData(new LoginResponseDto { Token = token!, User = userGetDto },
                AuthServiceMessages.VerificationSuccessful);
        }
        catch (ValidationException ex)
        {
            result.Fail(new ErrorMessage(ex.ExceptionCode, ex.Message));
        }
        catch (Exception ex)
        {
            result.Fail(new ErrorMessage("AUTH-562594", ex.Message));
        }

        return result;
    }

    private static string GenerateMfaCode()
    {
        var code = new Random().Next(100000, 999999).ToString();
        return code;
    }

    private async Task<string?> CheckIfUsernameRegisteredBefore(string username)
    {
        var user = await _userDal.GetAsync(p => p.Username == username);
        return user != null ? AuthServiceMessages.UsernameAlreadyRegistered : null;
    }

    private async Task<string?> CheckIfEmailRegisteredBefore(string email)
    {
        var user = await _userDal.GetAsync(p => p.Email == email);
        return user != null ? AuthServiceMessages.EmailAlreadyRegistered : null;
    }
}
using System.Text.Json;
using AutoMapper;
using Business.Constants.Messages.Services.Authentication;
using Business.Services.Authentication.Abstract;
using Business.Services.Communication.Abstract;
using Business.Utils.Validation.FluentValidation.Authentication;
using Core.Aspects.Validation;
using Core.Constants;
using Core.ExceptionHandling;
using Core.Extensions;
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

    [ValidationAspect(typeof(UserLoginValidator))]
    public async Task<ServiceObjectResult<Token?>> LoginUser(UserLoginDto userLoginDto)
    {
        var result = new ServiceObjectResult<Token?>();

        try
        {
            if (userLoginDto.Email != null)
                BusinessRules.Run(("AUTH-589819", BusinessRules.CheckEmail(userLoginDto.Email)));

            var user = await _userDal.GetAsync(
                u => u.Email == userLoginDto.Email || u.Username == userLoginDto.Username);

            if (user == null)
            {
                result.Fail(new ErrorMessage("AUTH-559600", AuthServiceMessages.NotFound));
                return result;
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                result.Fail(new ErrorMessage("AUTH-374015", AuthServiceMessages.WrongPassword));
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

            var token = _tokenHandler.GenerateToken(user.Id.ToString(), user.Username, user.Email, user.Role, false);

            var serializedToken = JsonSerializer.Serialize(token);
            user.ActiveToken = serializedToken;
            await _userDal.UpdateAsync(user);

            result.SetData(token, AuthServiceMessages.LoginSuccessful);
        }
        catch (ValidationException ex)
        {
            result.Fail(new ErrorMessage(ex.ExceptionCode, ex.Message));
        }
        catch (Exception ex)
        {
            result.Fail(new ErrorMessage("AUTH-425609", ex.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<bool>> LogoutUser(string userId)
    {
        var result = new ServiceObjectResult<bool>();
        try
        {
            BusinessRules.Run(("AUTH-249054", BusinessRules.CheckId(userId)));

            var isGlobalAdmin = AuthHelper.IsLoggedInAsAdmin();

            if (!isGlobalAdmin)
                BusinessRules.Run(("AUTH-239831", BusinessRules.CheckIdSameWithCurrentUser(userId)));

            var user = await _userDal.GetAsync(p => p.Id.Same(userId));
            BusinessRules.Run(("AUTH-261163", BusinessRules.CheckEntityNull(user)));

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
            result.Fail(new ErrorMessage("AUTH-949599", ex.Message));
        }

        return result;
    }

    [ValidationAspect(typeof(BusinessRegisterValidator))]
    public async Task<ServiceObjectResult<bool>> RegisterBusiness(BusinessRegisterDto businessRegisterDto)
    {
        var result = new ServiceObjectResult<bool>();

        try
        {
            BusinessRules.Run(
                ("AUTH-155448", BusinessRules.CheckDtoNull(businessRegisterDto)),
                ("AUTH-807859", BusinessRules.CheckEmail(businessRegisterDto.Email)),
                ("AUTH-970705", await CheckIfEmailRegisteredBefore(businessRegisterDto.Email)),
                ("AUTH-487889", await CheckIfUsernameRegisteredBefore(businessRegisterDto.Username))
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
            result.Fail(new ErrorMessage("AUTH-474544", ex.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<bool>> RegisterCustomer(CustomerRegisterDto customerRegisterDto)
    {
        var result = new ServiceObjectResult<bool>();

        try
        {
            BusinessRules.Run(
                ("AUTH-155448", BusinessRules.CheckDtoNull(customerRegisterDto)),
                ("AUTH-807859", BusinessRules.CheckEmail(customerRegisterDto.Email)),
                ("AUTH-970705", await CheckIfEmailRegisteredBefore(customerRegisterDto.Email)),
                ("AUTH-487889", await CheckIfUsernameRegisteredBefore(customerRegisterDto.Username))
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
            result.Fail(new ErrorMessage("AUTH-474544", ex.Message));
        }

        return result;
    }


    [ValidationAspect(typeof(VerifyEmailCodeValidator))]
    public async Task<ServiceObjectResult<Token?>> VerifyEmailCode(VerifyEmailCodeDto verifyCodeDto)
    {
        var result = new ServiceObjectResult<Token?>();
        try
        {
            var user = await _userDal.GetAsync(p => p.Email == verifyCodeDto.Email);
            if (user == null)
            {
                result.Fail(new ErrorMessage("AUTH-858063", AuthServiceMessages.NotFound));
                return result;
            }

            if (user.LoginVerificationCode != verifyCodeDto.Code)
            {
                result.Fail(new ErrorMessage("AUTH-107587", AuthServiceMessages.WrongVerificationCode));
                return result;
            }

            if (user.LoginVerificationCodeExpiration < DateTime.UtcNow)
            {
                result.Fail(new ErrorMessage("AUTH-639038", AuthServiceMessages.VerificationCodeExpired));
                return result;
            }

            user.LoginVerificationCode = null;
            user.LoginVerificationCodeExpiration = null;

            await _userDal.UpdateAsync(user);

            var token = _tokenHandler.GenerateToken(user.Id.ToString(), user.Username, user.Email, user.Role, false);

            result.SetData(token, AuthServiceMessages.LoginSuccessful);
        }
        catch (ValidationException ex)
        {
            result.Fail(new ErrorMessage(ex.ExceptionCode, ex.Message));
        }
        catch (Exception ex)
        {
            result.Fail(new ErrorMessage("AUTH-871272", ex.Message));
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
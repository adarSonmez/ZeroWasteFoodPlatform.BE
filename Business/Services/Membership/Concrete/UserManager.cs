using AutoMapper;
using Business.Constants.Messages.Services.Membership;
using Business.Services.Membership.Abstract;
using Business.Utils.Validation.FluentValidation.Membership;
using Core.Aspects.Validation;
using Core.ExceptionHandling;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.Hashing;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Membership;
using Domain.DTOs.Membership;

namespace Business.Services.Membership.Concrete;

public class UserManager : IUserService
{
    private readonly IBusinessDal _businessDal = ServiceTool.GetService<IBusinessDal>()!;
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;

    [ValidationAspect(typeof(UserChangePasswordValidator))]
    public async Task<ServiceObjectResult<UserGetDto?>> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto)
    {
        var result = new ServiceObjectResult<UserGetDto?>();

        try
        {
            BusinessRules.Run(
                ("USER-650320", BusinessRules.CheckDtoNull(userChangePasswordDto)),
                ("USER-650320", BusinessRules.CheckId(userChangePasswordDto.Id.ToString())),
                ("USER-650320",
                    userChangePasswordDto.NewPassword.Equals(userChangePasswordDto.ConfirmPassword)
                        ? null
                        : UserServiceMessages.PasswordsNotMatch));

            var user = await _businessDal.GetAsync(b => userChangePasswordDto.Id.Equals(b.Id));
            BusinessRules.Run(("USER-387257", BusinessRules.CheckEntityNull(user)));

            var passwordCheck = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword,
                user!.PasswordHash,
                user.PasswordSalt);

            BusinessRules.Run(("USER-650320", passwordCheck ? null : UserServiceMessages.IncorrectPassword));

            HashingHelper.CreatePasswordHash(userChangePasswordDto.NewPassword, out var passwordHash,
                out var passwordSalt);

            user!.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var userGetDto = _mapper.Map<UserGetDto>(user);
            result.SetData(userGetDto, UserServiceMessages.PasswordChanged);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("USER-650320", e.Message));
        }

        return result;
    }
}
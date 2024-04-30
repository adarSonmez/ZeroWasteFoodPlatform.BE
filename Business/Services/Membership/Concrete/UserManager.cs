using AutoMapper;
using Business.Constants.Messages.Services.Membership;
using Business.Services.Membership.Abstract;
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
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly IUserDal _userDal = ServiceTool.GetService<IUserDal>()!;

    public async Task<ServiceObjectResult<UserGetDto?>> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto)
    {
        var result = new ServiceObjectResult<UserGetDto?>();

        try
        {
            BusinessRules.Run(
                ("USER-342385", BusinessRules.CheckDtoNull(userChangePasswordDto)),
                ("USER-324854", BusinessRules.CheckId(userChangePasswordDto.Id.ToString())),
                ("USER-128747",
                    userChangePasswordDto.NewPassword.Equals(userChangePasswordDto.ConfirmPassword)
                        ? null
                        : UserServiceMessages.PasswordsNotMatch));

            var user = await _userDal.GetAsync(b => userChangePasswordDto.Id.Equals(b.Id));
            BusinessRules.Run(("USER-500620", BusinessRules.CheckEntityNull(user)));

            var passwordCheck = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword,
                user!.PasswordHash,
                user.PasswordSalt);

            BusinessRules.Run(("USER-569667", passwordCheck ? null : UserServiceMessages.IncorrectPassword));

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
            result.Fail(new ErrorMessage("USER-841618", e.Message));
        }

        return result;
    }
}
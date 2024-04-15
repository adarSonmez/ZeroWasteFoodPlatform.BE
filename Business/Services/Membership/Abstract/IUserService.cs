using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Membership;

namespace Business.Services.Membership.Abstract;

public interface IUserService : IService
{
    Task<ServiceObjectResult<UserGetDto?>> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto);
}
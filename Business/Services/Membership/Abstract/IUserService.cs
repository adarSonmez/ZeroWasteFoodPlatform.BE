using Core.Domain.Abstract;
using Core.Services.Result;
using Domain.DTOs.Membership;

namespace Business.Services.Membership.Abstract;

public interface IUserService : IDto
{
    Task<ServiceObjectResult<UserGetDto?>> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto);
}
using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Authentication;

namespace Business.Services.Authentication.Abstract;

public interface IAuthService : IService
{
    Task<ServiceObjectResult<LoginResponseDto?>> LoginUser(UserLoginDto userLoginDto);

    Task<ServiceObjectResult<bool>> LogoutUser(Guid userId);

    Task<ServiceObjectResult<bool>> RegisterBusiness(BusinessRegisterDto businessRegisterDto);

    Task<ServiceObjectResult<bool>> RegisterCustomer(CustomerRegisterDto customerRegisterDto);

    Task<ServiceObjectResult<LoginResponseDto?>> VerifyEmailCode(VerifyEmailCodeDto verifyCodeDto);
}
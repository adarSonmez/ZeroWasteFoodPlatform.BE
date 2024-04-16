using Core.Security.SessionManagement;
using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Authentication;

namespace Business.Services.Authentication.Abstract;

public interface IAuthService : IService
{
    Task<ServiceObjectResult<Token?>> LoginUser(UserLoginDto userLoginDto);

    Task<ServiceObjectResult<bool>> LogoutUser(string userId);

    Task<ServiceObjectResult<bool>> RegisterBusiness(BusinessRegisterDto businessRegisterDto);

    Task<ServiceObjectResult<bool>> RegisterCustomer(CustomerRegisterDto customerRegisterDto);

    Task<ServiceObjectResult<Token?>> VerifyEmailCode(VerifyEmailCodeDto verifyCodeDto);
}
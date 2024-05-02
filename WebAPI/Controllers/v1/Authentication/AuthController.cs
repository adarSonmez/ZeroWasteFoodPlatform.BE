using Business.Services.Authentication.Abstract;
using Core.Api.Abstract;
using Core.Utils.IoC;
using Domain.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Authentication;

[ApiController]
[Authorize]
public class AuthController : BaseController
{
    private readonly IAuthService _authService = ServiceTool.GetService<IAuthService>()!;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginUser(userLoginDto);

        if (result.HasFailed)
            return BadRequest(result);

        if (!result.ExtraData.Any())
            return Ok(result);

        return result.ExtraData.Any(extraData => extraData.Key == "useMFA" && (bool)extraData.Value!)
            ? StatusCode(428, result) // 428 Precondition Required
            : Ok(result);
    }

    [HttpPost("logout-user/{userId}")]
    public async Task<IActionResult> LogoutUser(string userId)
    {
        var result = await _authService.LogoutUser(userId);

        if (result.HasFailed)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("verify-code")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyCode(VerifyEmailCodeDto verifyCodeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.VerifyEmailCode(verifyCodeDto);

        if (result.HasFailed)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("register-business")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterBusiness([FromBody] BusinessRegisterDto businessRegisterDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterBusiness(businessRegisterDto);

        if (result.HasFailed)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("register-customer")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegisterDto customerRegisterDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterCustomer(customerRegisterDto);

        if (result.HasFailed)
            return BadRequest(result);

        return Ok(result);
    }
}
using Business.Services.Membership.Abstract;
using Core.Api.Abstract;
using Core.Utils.IoC;
using Domain.DTOs.Membership;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Membership;

[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _userService = ServiceTool.GetService<IUserService>()!;

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordDto userChangePasswordDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.ChangePasswordAsync(userChangePasswordDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
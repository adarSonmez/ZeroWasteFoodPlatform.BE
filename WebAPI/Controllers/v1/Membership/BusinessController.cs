using Business.Services.Membership.Abstract;
using Core.Api.Abstract;
using Core.Constants;
using Core.Utils.IoC;
using Domain.DTOs.Membership;
using Domain.FilterModels.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Membership;

[ApiController]
public class BusinessController : BaseController
{
    private readonly IBusinessService _businessService = ServiceTool.GetService<IBusinessService>()!;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _businessService.GetByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var result = await _businessService.GetByUsernameAsync(username);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _businessService.GetByEmailAsync(email);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] BusinessFilterModel? filterModel, int page = 1,
        int pageSize = 10)
    {
        var result = await _businessService.GetListAsync(filterModel, page, pageSize);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPut]
    [Authorize(Policy = AuthPolicies.AdminOrBusiness)]
    public async Task<IActionResult> Update([FromBody] BusinessUpdateDto businessUpdateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _businessService.UpdateAsync(businessUpdateDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = AuthPolicies.AdminOrBusiness)]
    public async Task<IActionResult> DeleteById(string id)
    {
        var result = await _businessService.DeleteByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
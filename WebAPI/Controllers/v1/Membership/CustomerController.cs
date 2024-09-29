using Business.Services.Membership.Abstract;
using Core.Api.Abstract;
using Core.Constants.StringConstants;
using Core.Utils.IoC;
using Domain.DTOs.Membership;
using Domain.FilterModels.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Membership;

[ApiController]
public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService = ServiceTool.GetService<ICustomerService>()!;

    [HttpGet("{id:guid}")]
    [Authorize(Policy = AuthPolicies.AdminOrCustomer)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _customerService.GetByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("username/{username}")]
    [Authorize(Policy = AuthPolicies.AdminOrCustomer)]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var result = await _customerService.GetByUsernameAsync(username);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("email/{email}")]
    [Authorize(Policy = AuthPolicies.AdminOrCustomer)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _customerService.GetByEmailAsync(email);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetList([FromQuery] CustomerFilterModel? filterModel, int page = 1,
        int pageSize = 10)
    {
        var result = await _customerService.GetListAsync(filterModel, page, pageSize);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("shopping-list/{userId:guid}")]
    [Authorize(Policy = AuthPolicies.AdminOrCustomer)]
    public async Task<IActionResult> GetShoppingList(Guid userId)
    {
        var result = await _customerService.GetShoppingListAsync(userId);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPut]
    [Authorize(Policy = AuthPolicies.AdminOrCustomer)]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateDto customerUpdateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _customerService.UpdateAsync(customerUpdateDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AuthPolicies.AdminOrCustomer)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var result = await _customerService.DeleteByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
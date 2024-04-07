using Business.Services.Membership.Abstract;
using Core.Api.Abstract;
using Core.Utils.IoC;
using Domain.DTOs.Membership;
using Domain.FilterModels.Membership;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Membership;

[ApiController]
public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService = ServiceTool.GetService<ICustomerService>()!;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _customerService.GetByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var result = await _customerService.GetByUsernameAsync(username);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _customerService.GetByEmailAsync(email);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] CustomerFilterModel? filterModel, int page = 1,
        int pageSize = 10)
    {
        var result = await _customerService.GetListAsync(filterModel, page, pageSize);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateDto customerUpdateDto)
    {
        var result = await _customerService.UpdateAsync(customerUpdateDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var result = await _customerService.DeleteByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
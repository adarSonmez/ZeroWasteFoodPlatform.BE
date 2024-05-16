using Business.Services.Marketing.Abstract;
using Core.Api.Abstract;
using Core.Constants;
using Core.Utils.IoC;
using Domain.DTOs.Marketing;
using Domain.FilterModels.Marketing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Marketing;

[ApiController]
public class StoreProductController : BaseController
{
    private readonly IStoreProductService _storeProductService = ServiceTool.GetService<IStoreProductService>()!;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _storeProductService.GetByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId, [FromQuery] StoreProductFilterModel? filterModel = null,
        int page = 1,
        int pageSize = 10)
    {
        var result = await _storeProductService.GetByUserIdAsync(userId, filterModel, page, pageSize);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] StoreProductFilterModel? filterModel = null, int page = 1,
        int pageSize = 10)
    {
        var result = await _storeProductService.GetListAsync(filterModel, page, pageSize);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = AuthPolicies.AdminOrBusiness)]
    public async Task<IActionResult> Add([FromBody] StoreProductAddDto storeProductAddDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _storeProductService.AddAsync(storeProductAddDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPost("add-to-shopping-list")]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<IActionResult> AddToShoppingList(
        [FromBody] StoreProductManipulateShoppingListDto storeProductManipulateShoppingListDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _storeProductService.AddToShoppingListAsync(storeProductManipulateShoppingListDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPost("remove-from-shopping-list")]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<IActionResult> RemoveFromShoppingList(
        [FromBody] StoreProductManipulateShoppingListDto storeProductManipulateShoppingListDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _storeProductService.RemoveFromShoppingListAsync(storeProductManipulateShoppingListDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPut]
    [Authorize(Policy = AuthPolicies.AdminOrBusiness)]
    public async Task<IActionResult> Update([FromBody] StoreProductUpdateDto storeProductUpdateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _storeProductService.UpdateAsync(storeProductUpdateDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AuthPolicies.AdminOrBusiness)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _storeProductService.DeleteByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
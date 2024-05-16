using Business.Services.Marketing.Abstract;
using Core.Api.Abstract;
using Core.Utils.IoC;
using Domain.DTOs.Marketing;
using Domain.FilterModels.Marketing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Marketing;

[ApiController]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService = ServiceTool.GetService<ICategoryService>()!;
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _categoryService.GetByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _categoryService.GetAllAsync();

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
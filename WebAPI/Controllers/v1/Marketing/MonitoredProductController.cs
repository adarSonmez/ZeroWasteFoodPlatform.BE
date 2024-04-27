using Business.Services.Marketing.Abstract;
using Core.Api.Abstract;
using Core.Utils.IoC;
using Domain.DTOs.Marketing;
using Domain.FilterModels.Marketing;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Marketing;

[ApiController]
public class MonitoredProductController : BaseController
{
    private readonly IMonitoredProductService _monitoredProductService = ServiceTool.GetService<IMonitoredProductService>()!;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _monitoredProductService.GetByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await _monitoredProductService.GetByUserIdAsync(userId);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] MonitoredProductFilterModel? filterModel = null, int page = 1,
        int pageSize = 10)
    {
        var result = await _monitoredProductService.GetListAsync(filterModel, page, pageSize);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] MonitoredProductAddDto monitoredProductAddDto)
    {
        var result = await _monitoredProductService.AddAsync(monitoredProductAddDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MonitoredProductUpdateDto monitoredProductUpdateDto)
    {
        var result = await _monitoredProductService.UpdateAsync(monitoredProductUpdateDto);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _monitoredProductService.DeleteByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
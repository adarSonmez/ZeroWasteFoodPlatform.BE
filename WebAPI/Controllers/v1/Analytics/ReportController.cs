using Business.Services.Analytics.Abstract;
using Core.Api.Abstract;
using Core.Constants;
using Core.Utils.IoC;
using Domain.FilterModels.Analytics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.Analytics;

[ApiController]
[Authorize(Policy = AuthPolicies.AdminOnly)]
public class ReportController : BaseController
{
    private readonly IReportService _reportService = ServiceTool.GetService<IReportService>()!;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _reportService.GetByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] ReportFilterModel? filterModel, int page = 1,
        int pageSize = 10)
    {
        var result = await _reportService.GetListAsync(filterModel, page, pageSize);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _reportService.DeleteByIdAsync(id);

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}
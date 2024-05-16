using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Analytics;
using Domain.FilterModels.Analytics;

namespace Business.Services.Analytics.Abstract;

public interface IReportService : IService
{
    Task<ServiceObjectResult<ReportGetDto?>> GetByIdAsync(Guid id);

    Task<ServiceCollectionResult<ReportGetDto>> GetListAsync(ReportFilterModel? filterModel, int page, int pageSize);

    // TODO: Report will be created by the AI service
    // Task<ServiceObjectResult<ReportGetDto?>> CreateAsync(ReportCreateDto reportCreateDto);

    Task<ServiceObjectResult<ReportGetDto?>> DeleteByIdAsync(Guid id);
}
using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Marketing;
using Domain.FilterModels.Marketing;

namespace Business.Services.Marketing.Abstract;

public interface IMonitoredProductService : IService
{
    Task<ServiceObjectResult<MonitoredProductGetDto?>> GetByIdAsync(Guid id);

    Task<ServiceCollectionResult<MonitoredProductGetDto>>
        GetListAsync(MonitoredProductFilterModel? filterModel, int page, int pageSize);

    Task<ServiceCollectionResult<MonitoredProductGetDto>> GetByUserIdAsync(Guid userId);

    Task<ServiceObjectResult<MonitoredProductGetDto?>> AddAsync(MonitoredProductAddDto productAddDto);

    Task<ServiceObjectResult<MonitoredProductGetDto?>> UpdateAsync(MonitoredProductUpdateDto productUpdateDto);

    Task<ServiceObjectResult<MonitoredProductGetDto?>> DeleteByIdAsync(Guid id);
}
using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Membership;
using Domain.FilterModels.Membership;

namespace Business.Services.Membership.Abstract;

public interface IBusinessService : IService
{
    Task<ServiceObjectResult<BusinessGetDto?>> GetByIdAsync(string id);

    Task<ServiceObjectResult<BusinessGetDto?>> GetByUsernameAsync(string username);

    Task<ServiceObjectResult<BusinessGetDto?>> GetByEmailAsync(string email);

    Task<ServiceCollectionResult<BusinessGetDto>>
        GetListAsync(BusinessFilterModel? filterModel, int page, int pageSize);

    Task<ServiceObjectResult<BusinessGetDto?>> UpdateAsync(BusinessUpdateDto businessUpdateDto);

    Task<ServiceObjectResult<BusinessGetDto?>> DeleteByIdAsync(string id);
}
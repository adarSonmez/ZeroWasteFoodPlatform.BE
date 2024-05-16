using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Marketing;
using Domain.DTOs.Membership;
using Domain.FilterModels.Membership;

namespace Business.Services.Membership.Abstract;

public interface ICustomerService : IService
{
    Task<ServiceObjectResult<CustomerGetDto?>> GetByIdAsync(Guid id);

    Task<ServiceObjectResult<CustomerGetDto?>> GetByUsernameAsync(string username);

    Task<ServiceObjectResult<CustomerGetDto?>> GetByEmailAsync(string email);

    Task<ServiceCollectionResult<StoreProductGetDto>> GetShoppingListAsync(Guid userId);

    Task<ServiceCollectionResult<CustomerGetDto>>
        GetListAsync(CustomerFilterModel? filterModel, int page, int pageSize);

    Task<ServiceObjectResult<CustomerGetDto?>> UpdateAsync(CustomerUpdateDto customerUpdateDto);

    Task<ServiceObjectResult<CustomerGetDto?>> DeleteByIdAsync(Guid id);
}
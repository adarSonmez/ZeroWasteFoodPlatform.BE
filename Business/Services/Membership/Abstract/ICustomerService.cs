using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Membership;

namespace Business.Services.Membership.Abstract;

public interface ICustomerService : IService
{
    Task<ServiceObjectResult<CustomerGetDto?>> GetByIdAsync(string id);

    Task<ServiceObjectResult<CustomerGetDto?>> GetByUsernameAsync(string username);

    Task<ServiceObjectResult<CustomerGetDto?>> GetByEmailAsync(string email);

    Task<ServiceCollectionResult<CustomerGetDto>> GetAllAsync();

    Task<ServiceObjectResult<CustomerGetDto?>> UpdateAsync(CustomerUpdateDto customerUpdateDto);

    Task<ServiceObjectResult<CustomerGetDto?>> DeleteByIdAsync(string id);
}
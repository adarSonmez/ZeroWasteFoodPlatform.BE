using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Marketing;

namespace Business.Services.Marketing.Abstract;

public interface ICategoryService : IService
{
    Task<ServiceObjectResult<CategoryGetDto?>> GetByIdAsync(Guid id);
    Task<ServiceCollectionResult<CategoryGetDto?>> GetAllAsync();
}
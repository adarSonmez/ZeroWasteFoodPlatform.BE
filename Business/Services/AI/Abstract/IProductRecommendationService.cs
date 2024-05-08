using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Marketing;

namespace Business.Services.AI.Abstract;

public interface IProductRecommendationService : IService
{
    Task<ServiceCollectionResult<StoreProductGetDto>> GetRecommendedProductsAsync();
}
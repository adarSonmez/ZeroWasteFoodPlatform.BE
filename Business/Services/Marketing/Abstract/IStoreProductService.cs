using Core.Services;
using Core.Services.Result;
using Domain.DTOs.Marketing;
using Domain.FilterModels.Marketing;

namespace Business.Services.Marketing.Abstract;

public interface IStoreProductService : IService
{
    Task<ServiceObjectResult<StoreProductGetDto?>> GetByIdAsync(Guid id);

    Task<ServiceCollectionResult<StoreProductGetDto>> GetListAsync(StoreProductFilterModel? filterModel, int page,
        int pageSize);

    Task<ServiceCollectionResult<StoreProductGetDto>> GetByUserIdAsync(Guid userId,
        StoreProductFilterModel? filterModel,
        int page, int pageSize);

    Task<ServiceObjectResult<StoreProductGetDto?>> AddAsync(StoreProductAddDto productAddDto);

    Task<ServiceObjectResult<StoreProductGetDto?>> AddToShoppingListAsync(
        StoreProductManipulateShoppingListDto productManipulateShoppingListDto);

    Task<ServiceObjectResult<StoreProductGetDto?>> RemoveFromShoppingListAsync(
        StoreProductManipulateShoppingListDto productManipulateShoppingListDto);

    Task<ServiceCollectionResult<IList<Guid>>> GetAllShoppingListsAnonymously();

    Task<ServiceObjectResult<StoreProductGetDto?>> UpdateAsync(StoreProductUpdateDto productUpdateDto);

    Task<ServiceObjectResult<StoreProductGetDto?>> DeleteByIdAsync(Guid id);

    Task<ServiceObjectResult<StoreProductGetDto?>> GetByBarcodeAsync(string barcode);
}
using AutoMapper;
using Business.Constants.Messages.Services.Marketing;
using Business.Services.Marketing.Abstract;
using Core.ExceptionHandling;
using Core.Extensions;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.Auth;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Association;
using DataAccess.Repositories.Abstract.Marketing;
using Domain.DTOs.Marketing;
using Domain.Entities.Association;
using Domain.Entities.Marketing;
using Domain.FilterModels.Marketing;

namespace Business.Services.Marketing.Concrete;

public class StoreProductManager : IStoreProductService
{
    private readonly ICustomerStoreProductDal _customerStoreProductDal =
        ServiceTool.GetService<ICustomerStoreProductDal>()!;

    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly IStoreProductDal _storeProductDal = ServiceTool.GetService<IStoreProductDal>()!;
    private readonly ICategoryProductDal _categoryProductDal = ServiceTool.GetService<ICategoryProductDal>()!;
    private readonly ICategoryDal _categoryDal = ServiceTool.GetService<ICategoryDal>()!;

    public async Task<ServiceObjectResult<StoreProductGetDto?>> GetByIdAsync(string id)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            BusinessRules.Run(("STPR-218236", BusinessRules.CheckId(id)));

            var storeProduct = await _storeProductDal.GetAsync(b => b.Id.ToString().Equals(id));
            BusinessRules.Run(("STPR-194703", BusinessRules.CheckEntityNull(storeProduct)));
            
            // Add category product to store product
            var categoryProducts = await _categoryProductDal.GetAllAsync(b => b.ProductId.ToString() == storeProduct!.Id.ToString());
            var categories = await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));
            
            storeProduct!.Categories = categories;

            var storeProductGetDto = _mapper.Map<StoreProductGetDto>(storeProduct);
            result.SetData(storeProductGetDto, StoreProductServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-121349", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<StoreProductGetDto>> GetByUserIdAsync(string userId)
    {
        var result = new ServiceCollectionResult<StoreProductGetDto>();

        try
        {
            BusinessRules.Run(("STPR-171373", BusinessRules.CheckId(userId)));

            var products = await _storeProductDal.GetAllAsync(b => b.BusinessId.ToString().Equals(userId));
            
            foreach (var product in products)
            {
                var categoryProducts = await _categoryProductDal.GetAllAsync(b => b.ProductId.ToString() == product.Id.ToString());
                var categories = await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));
                
                product.Categories = categories;
            }
            
            var productGetDtos = _mapper.Map<List<StoreProductGetDto>>(products);
            result.SetData(productGetDtos, successMessage: StoreProductServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-772325", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<StoreProductGetDto?>> DeleteByIdAsync(string id)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            BusinessRules.Run(("STPR-748260", BusinessRules.CheckId(id)));
            var product = await _storeProductDal.GetAsync(b => b.Id.ToString().Equals(id));
            BusinessRules.Run(("STPR-735131", BusinessRules.CheckEntityNull(product)));

            await _storeProductDal.SoftDeleteAsync(product!);
            result.SetData(_mapper.Map<StoreProductGetDto>(product), StoreProductServiceMessages.Deleted);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-458451", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<StoreProductGetDto>> GetListAsync(
        StoreProductFilterModel? filterModel, int page, int pageSize)
    {
        var result = new ServiceCollectionResult<StoreProductGetDto>();

        try
        {
            var filters = filterModel?.ToExpression();
            var products = await _storeProductDal.GetAllAsync(filters);
            
            foreach (var product in products)
            {
                var categoryProducts = await _categoryProductDal.GetAllAsync(b => b.ProductId.ToString() == product.Id.ToString());
                var categories = await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));
                
                product.Categories = categories;
            }
            
            var productGetDtos = _mapper.Map<List<StoreProductGetDto>>(products);
            result.SetData(productGetDtos, page, pageSize, StoreProductServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-550140", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<StoreProductGetDto?>> AddAsync(StoreProductAddDto productAddDto)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            BusinessRules.Run(("STPR-412438", BusinessRules.CheckDtoNull(productAddDto)));
            var product = _mapper.Map<StoreProduct>(productAddDto);
            var currentUserId = Guid.Parse(AuthHelper.GetUserId()!);
            product.BusinessId = currentUserId;
            product.CreatedUserId = currentUserId;

            await _storeProductDal.AddAsync(product);
            result.SetData(_mapper.Map<StoreProductGetDto>(product), StoreProductServiceMessages.Added);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-274294", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<StoreProductGetDto?>> AddToShoppingListAsync(
        StoreProductManipulateShoppingListDto productManipulateShoppingListDto)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            BusinessRules.Run(("STPR-646480", BusinessRules.CheckDtoNull(productManipulateShoppingListDto)));

            var product = await _storeProductDal.GetAsync(b =>
                b.Id.ToString().Equals(productManipulateShoppingListDto.ProductId.ToString()));
            BusinessRules.Run(("STPR-591299", BusinessRules.CheckEntityNull(product)));

            var currentUserId = Guid.Parse(AuthHelper.GetUserId()!);
            var customerStoreProduct = new CustomerStoreProduct
            {
                CustomerId = currentUserId,
                ProductId = product!.Id
            };

            await _customerStoreProductDal.AddAsync(customerStoreProduct);

            result.SetData(_mapper.Map<StoreProductGetDto>(product), StoreProductServiceMessages.AddedToShoppingList);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-136340", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<StoreProductGetDto?>> RemoveFromShoppingListAsync(
        StoreProductManipulateShoppingListDto productManipulateShoppingListDto)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            BusinessRules.Run(("STPR-612880", BusinessRules.CheckDtoNull(productManipulateShoppingListDto)));

            var product = await _storeProductDal.GetAsync(b =>
                b.Id.ToString().Equals(productManipulateShoppingListDto.ProductId.ToString()));
            BusinessRules.Run(("STPR-794153", BusinessRules.CheckEntityNull(product)));

            var currentUserId = Guid.Parse(AuthHelper.GetUserId()!);
            var customerStoreProduct = await _customerStoreProductDal.GetAsync(b =>
                b.CustomerId.ToString().Equals(currentUserId.ToString()) &&
                b.ProductId.ToString().Equals(product!.Id.ToString()));

            BusinessRules.Run(("STPR-612880", BusinessRules.CheckEntityNull(customerStoreProduct)));

            await _customerStoreProductDal.SoftDeleteAsync(customerStoreProduct!);

            result.SetData(_mapper.Map<StoreProductGetDto>(product),
                StoreProductServiceMessages.RemovedFromShoppingList);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-884821", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<StoreProductGetDto?>> UpdateAsync(
        StoreProductUpdateDto productUpdateDto)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            BusinessRules.Run(
                ("STPR-538394", BusinessRules.CheckDtoNull(productUpdateDto)),
                ("STPR-217169", BusinessRules.CheckId(productUpdateDto.Id.ToString()))
            );

            var product = await _storeProductDal.GetAsync(b => b.Id.ToString().Equals(productUpdateDto.Id.ToString()));
            BusinessRules.Run(("STPR-780245", BusinessRules.CheckEntityNull(product)));

            _mapper.Map(productUpdateDto, product);
            await _storeProductDal.UpdateAsync(product!);
            result.SetData(_mapper.Map<StoreProductGetDto>(product), StoreProductServiceMessages.Updated);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-348787", e.Message));
        }

        return result;
    }
}
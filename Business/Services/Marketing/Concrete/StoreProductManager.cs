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
using DataAccess.Repositories.Abstract.Membership;
using Domain.DTOs.Marketing;
using Domain.Entities.Association;
using Domain.Entities.Marketing;
using Domain.FilterModels.Marketing;

namespace Business.Services.Marketing.Concrete;

public class StoreProductManager : IStoreProductService
{
    private readonly IBusinessDal _businessDal = ServiceTool.GetService<IBusinessDal>()!;
    private readonly ICategoryDal _categoryDal = ServiceTool.GetService<ICategoryDal>()!;
    private readonly ICategoryProductDal _categoryProductDal = ServiceTool.GetService<ICategoryProductDal>()!;

    private readonly ICustomerStoreProductDal _customerStoreProductDal =
        ServiceTool.GetService<ICustomerStoreProductDal>()!;

    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly IStoreProductDal _storeProductDal = ServiceTool.GetService<IStoreProductDal>()!;

    public async Task<ServiceObjectResult<StoreProductGetDto?>> GetByIdAsync(Guid id)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            var storeProduct = await _storeProductDal.GetAsync(b => b.Id.Equals(id));
            BusinessRules.Run(("STPR-194703", BusinessRules.CheckEntityNull(storeProduct)));

            // Add category product to store product
            var categoryProducts =
                await _categoryProductDal.GetAllAsync(b => b.ProductId.Equals(storeProduct!.Id));
            var categories =
                await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

            storeProduct!.Categories = categories;

            var business = await _businessDal.GetAsync(b => b.Id.Equals(storeProduct.BusinessId));
            BusinessRules.Run(("STPR-194703", BusinessRules.CheckEntityNull(business)));

            storeProduct.Business = business!;

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

    public async Task<ServiceCollectionResult<StoreProductGetDto>> GetByUserIdAsync(Guid userId,
        StoreProductFilterModel? filterModel,
        int page, int pageSize)
    {
        var result = new ServiceCollectionResult<StoreProductGetDto>();

        try
        {
            var filters = filterModel?.ToExpression();

            var products = await _storeProductDal.GetAllAsync(filters);
            products = products.Where(p => p.BusinessId.Equals(userId)).ToList();

            foreach (var product in products)
            {
                var categoryProducts =
                    await _categoryProductDal.GetAllAsync(b => b.ProductId.Equals(product.Id));
                var categories =
                    await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

                product.Categories = categories;

                var business = await _businessDal.GetAsync(b => b.Id.Equals(product.BusinessId));
                BusinessRules.Run(("STPR-125496", BusinessRules.CheckEntityNull(business)));

                product.Business = business!;
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
            result.Fail(new ErrorMessage("STPR-772325", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<StoreProductGetDto?>> DeleteByIdAsync(Guid id)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            var product = await _storeProductDal.GetAsync(b => b.Id.Equals(id));
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

    public async Task<ServiceObjectResult<StoreProductGetDto?>> GetByBarcodeAsync(string barcode)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            var product = await _storeProductDal.GetAsync(b => barcode.Equals(b.Barcode));
            BusinessRules.Run(("STPR-255461", BusinessRules.CheckEntityNull(product)));

            var categoryProducts =
                await _categoryProductDal.GetAllAsync(b => b.ProductId.Equals(product!.Id));
            var categories =
                await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

            product!.Categories = categories;

            var business = await _businessDal.GetAsync(b => b.Id.Equals(product.BusinessId));
            BusinessRules.Run(("STPR-125488", BusinessRules.CheckEntityNull(business)));

            product.Business = business!;

            result.SetData(_mapper.Map<StoreProductGetDto>(product), StoreProductServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-996542", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<StoreProductGetDto>> GetListAsync(
        StoreProductFilterModel? filterModel, string? sortBy,
        string? categoryIds, string? storeIds, double? percentDiscountLow, double? percentDiscountHigh,
        decimal? originalPriceLow, decimal? originalPriceHigh, int page, int pageSize)
    {
        var result = new ServiceCollectionResult<StoreProductGetDto>();

        try
        {
            Func<IQueryable<StoreProduct>, IOrderedQueryable<StoreProduct>>? orderBy = null;

            if (sortBy != null)
                orderBy = sortBy switch
                {
                    "name" => q => q.OrderBy(p => p.Name),
                    "price" => q => q.OrderBy(p => p.OriginalPrice),
                    "discount" => q => q.OrderByDescending(p => p.PercentDiscount),
                    _ => null
                };

            var filters = filterModel?.ToExpression();
            var products = await _storeProductDal.GetAllAsync(filters, orderBy: orderBy);

            if (categoryIds != null)
            {
                var categoryIdsList = categoryIds.Split(',').Select(Guid.Parse).ToList();
                var categoryProducts =
                    await _categoryProductDal.GetAllAsync(b => categoryIdsList.Contains(b.CategoryId));
                var productIds = categoryProducts.Select(c => c.ProductId).ToList();
                products = products.Where(p => productIds.Contains(p.Id)).ToList();
            }

            if (storeIds != null)
            {
                var storeIdsList = storeIds.Split(',').Select(Guid.Parse).ToList();
                products = products.Where(p => storeIdsList.Contains(p.BusinessId)).ToList();
            }

            if (percentDiscountLow != null)
                products = products.Where(p => p.PercentDiscount >= percentDiscountLow).ToList();

            if (percentDiscountHigh != null)
                products = products.Where(p => p.PercentDiscount <= percentDiscountHigh).ToList();

            if (originalPriceLow != null)
                products = products.Where(p => p.OriginalPrice >= originalPriceLow).ToList();

            if (originalPriceHigh != null)
                products = products.Where(p => p.OriginalPrice <= originalPriceHigh).ToList();

            foreach (var product in products)
            {
                var categoryProducts =
                    await _categoryProductDal.GetAllAsync(b => b.ProductId.Equals(product.Id));
                var categories =
                    await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

                product.Categories = categories;

                var business = await _businessDal.GetAsync(b => b.Id.Equals(product.BusinessId));
                BusinessRules.Run(("STPR-515321", BusinessRules.CheckEntityNull(business)));

                product.Business = business!;
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
            BusinessRules.Run(("STPR-255225", await CheckIfBarcodeExists(productAddDto.Barcode)));

            var product = _mapper.Map<StoreProduct>(productAddDto);

            var currentUserId = AuthHelper.GetUserId() ?? throw new ValidationException("STPR-145689",
                StoreProductServiceMessages.UserNotFound);

            product.BusinessId = currentUserId;
            product.CreatedUserId = currentUserId;

            var categoryIds = productAddDto.CategoriesIds;

            // TODO: Sent from flutter app without guid's but category names. Fix this.
            if (categoryIds.Count != 0 && !Guid.TryParse(categoryIds.First(), out _))
            {
                var categories = await _categoryDal.GetAllAsync(b => categoryIds.Contains(b.Name));
                categoryIds = categories.Select(c => c.Id.ToString()).ToList();
            }

            await _storeProductDal.AddAsync(product);

            foreach (var categoryId in categoryIds)
            {
                var categoryProduct = new CategoryProduct
                {
                    ProductId = product.Id,
                    CategoryId = Guid.Parse(categoryId)
                };

                await _categoryProductDal.AddAsync(categoryProduct);
            }

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
                b.Id.Equals(productManipulateShoppingListDto.ProductId));
            BusinessRules.Run(("STPR-591299", BusinessRules.CheckEntityNull(product)));

            var categoryProducts =
                await _categoryProductDal.GetAllAsync(b => b.ProductId.Equals(product!.Id));
            var categories =
                await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

            product!.Categories = categories;

            var business = await _businessDal.GetAsync(b => b.Id.Equals(product.BusinessId));
            BusinessRules.Run(("STPR-194703", BusinessRules.CheckEntityNull(business)));

            product.Business = business!;

            var currentUserId = AuthHelper.GetUserId() ?? throw new ValidationException("STPR-785463",
                StoreProductServiceMessages.UserNotFound);

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
                b.Id.Equals(productManipulateShoppingListDto.ProductId));
            BusinessRules.Run(("STPR-794153", BusinessRules.CheckEntityNull(product)));

            var currentUserId = AuthHelper.GetUserId() ?? throw new ValidationException("STPR-861862",
                StoreProductServiceMessages.UserNotFound);

            var customerStoreProduct = await _customerStoreProductDal.GetAsync(b =>
                b.CustomerId.Equals(currentUserId) &&
                b.ProductId.Equals(product!.Id));

            BusinessRules.Run(("STPR-612880", BusinessRules.CheckEntityNull(customerStoreProduct)));

            await _customerStoreProductDal.HardDeleteAsync(customerStoreProduct!);

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

    public async Task<ServiceCollectionResult<IList<Guid>>> GetAllShoppingListsAnonymously()
    {
        var result = new ServiceCollectionResult<IList<Guid>>();
        try
        {
            var customerStoreProducts = await _customerStoreProductDal.GetAllAsync();
            var groupedCustomerStoreProducts = customerStoreProducts.GroupBy(c => c.CustomerId);
            var productIds = groupedCustomerStoreProducts.Select(g => g.Select(c => c.ProductId).ToList())
                .ToList();
            result.SetData(productIds, successMessage: StoreProductServiceMessages.ShoppingListsRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("STPR-950070", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<StoreProductGetDto?>> UpdateAsync(
        StoreProductUpdateDto productUpdateDto)
    {
        var result = new ServiceObjectResult<StoreProductGetDto?>();

        try
        {
            BusinessRules.Run(("STPR-538394", BusinessRules.CheckDtoNull(productUpdateDto)));

            var product = await _storeProductDal.GetAsync(b => b.Id.Equals(productUpdateDto.Id));
            BusinessRules.Run(("STPR-780245", BusinessRules.CheckEntityNull(product)));

            _mapper.Map(productUpdateDto, product);

            var categoryIds = productUpdateDto.CategoryIds ?? new List<string>();

            // TODO: Sent from flutter app without guid's but category names. Fix this.
            if (categoryIds.Count != 0 && !Guid.TryParse(categoryIds.First(), out _))
            {
                var categories = await _categoryDal.GetAllAsync(b => categoryIds.Contains(b.Name));
                categoryIds = categories.Select(c => c.Id.ToString()).ToList();
            }

            foreach (var categoryId in categoryIds)
            {
                var categoryProduct = await _categoryProductDal.GetAsync(b =>
                    b.ProductId.Equals(product!.Id) && b.CategoryId.Equals(Guid.Parse(categoryId)));

                if (categoryProduct == null)
                    await _categoryProductDal.AddAsync(new CategoryProduct
                    {
                        ProductId = product!.Id,
                        CategoryId = Guid.Parse(categoryId)
                    });
            }

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

    private async Task<string?> CheckIfBarcodeExists(string barcode)
    {
        var product = await _storeProductDal.GetAsync(b => barcode.Equals(b.Barcode));
        return product != null ? StoreProductServiceMessages.BarcodeExists : null;
    }
}
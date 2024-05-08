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
using Domain.Entities.Marketing;
using Domain.FilterModels.Marketing;

namespace Business.Services.Marketing.Concrete;

public class MonitoredProductManager : IMonitoredProductService
{
    private readonly ICategoryDal _categoryDal = ServiceTool.GetService<ICategoryDal>()!;
    private readonly ICategoryProductDal _categoryProductDal = ServiceTool.GetService<ICategoryProductDal>()!;
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly IMonitoredProductDal _monitoredProductDal = ServiceTool.GetService<IMonitoredProductDal>()!;

    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> GetByIdAsync(string id)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(("MNPR-734047", BusinessRules.CheckId(id)));

            var monitoredProduct = await _monitoredProductDal.GetAsync(b => b.Id.ToString().Equals(id));
            BusinessRules.Run(("MNPR-987121", BusinessRules.CheckEntityNull(monitoredProduct)));

            var categoryProducts =
                await _categoryProductDal.GetAllAsync(b => b.ProductId.ToString() == monitoredProduct!.Id.ToString());
            var categories =
                await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

            monitoredProduct!.Categories = categories;

            var monitoredProductGetDto = _mapper.Map<MonitoredProductGetDto>(monitoredProduct);
            result.SetData(monitoredProductGetDto, MonitoredProductServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-376848", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<MonitoredProductGetDto>> GetListAsync(
        MonitoredProductFilterModel? filterModel, int page, int pageSize)
    {
        var result = new ServiceCollectionResult<MonitoredProductGetDto>();

        try
        {
            var filters = filterModel?.ToExpression();
            var products = await _monitoredProductDal.GetAllAsync(filters);

            foreach (var product in products)
            {
                var categoryProducts =
                    await _categoryProductDal.GetAllAsync(b => b.ProductId.ToString() == product.Id.ToString());
                var categories =
                    await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

                product.Categories = categories;
            }

            var productGetDtos = _mapper.Map<List<MonitoredProductGetDto>>(products);
            result.SetData(productGetDtos, page, pageSize, MonitoredProductServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-472873", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<MonitoredProductGetDto>> GetByUserIdAsync(string userId)
    {
        var result = new ServiceCollectionResult<MonitoredProductGetDto>();

        try
        {
            BusinessRules.Run(("MNPR-724642", BusinessRules.CheckId(userId)));

            var products = await _monitoredProductDal.GetAllAsync(b => b.OwnerId.ToString().Equals(userId));

            foreach (var product in products)
            {
                var categoryProducts =
                    await _categoryProductDal.GetAllAsync(b => b.ProductId.ToString() == product.Id.ToString());
                var categories =
                    await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

                product.Categories = categories;
            }

            var productGetDtos = _mapper.Map<List<MonitoredProductGetDto>>(products);
            result.SetData(productGetDtos, successMessage: MonitoredProductServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-814907", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> AddAsync(MonitoredProductAddDto productAddDto)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(("MNPR-612521", BusinessRules.CheckDtoNull(productAddDto)));
            var product = _mapper.Map<MonitoredProduct>(productAddDto);
            var currentUserId = Guid.Parse(AuthHelper.GetUserId()!);
            product.OwnerId = currentUserId;
            product.CreatedUserId = currentUserId;

            await _monitoredProductDal.AddAsync(product);
            result.SetData(_mapper.Map<MonitoredProductGetDto>(product), MonitoredProductServiceMessages.Added);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-646039", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> DeleteByIdAsync(string id)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(("MNPR-197002", BusinessRules.CheckId(id)));
            var product = await _monitoredProductDal.GetAsync(b => b.Id.ToString().Equals(id));
            BusinessRules.Run(("MNPR-591741", BusinessRules.CheckEntityNull(product)));

            await _monitoredProductDal.SoftDeleteAsync(product!);
            result.SetData(_mapper.Map<MonitoredProductGetDto>(product), MonitoredProductServiceMessages.Deleted);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-811620", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> UpdateAsync(
        MonitoredProductUpdateDto productUpdateDto)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(
                ("MNPR-308013", BusinessRules.CheckDtoNull(productUpdateDto)),
                ("MNPR-775480", BusinessRules.CheckId(productUpdateDto.Id.ToString()))
            );

            var product =
                await _monitoredProductDal.GetAsync(b => b.Id.ToString().Equals(productUpdateDto.Id.ToString()));
            BusinessRules.Run(("MNPR-927511", BusinessRules.CheckEntityNull(product)));

            _mapper.Map(productUpdateDto, product);
            await _monitoredProductDal.UpdateAsync(product!);
            result.SetData(_mapper.Map<MonitoredProductGetDto>(product), MonitoredProductServiceMessages.Updated);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-944544", e.Message));
        }

        return result;
    }
}
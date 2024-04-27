using AutoMapper;
using Business.Constants.Messages.Services.Marketing;
using Business.Services.Marketing.Abstract;
using Business.Utils.Validation.FluentValidation.Marketing;
using Core.Aspects.Validation;
using Core.ExceptionHandling;
using Core.Extensions;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Marketing;
using Domain.DTOs.Marketing;
using Domain.Entities.Marketing;
using Domain.FilterModels.Marketing;

namespace Business.Services.Marketing.Concrete;

public class MonitoredProductManager : IMonitoredProductService
{
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly IMonitoredProductDal _monitoredProductDal = ServiceTool.GetService<IMonitoredProductDal>()!;

    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> GetByIdAsync(string id)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(("MNPR-950070", BusinessRules.CheckId(id)));

            var monitoredProduct = await _monitoredProductDal.GetAsync(b => b.Id.ToString().Equals(id));
            BusinessRules.Run(("MNPR-759000", BusinessRules.CheckEntityNull(monitoredProduct)));

            var monitoredProductGetDto = _mapper.Map<MonitoredProductGetDto>(monitoredProduct);
            result.SetData(monitoredProductGetDto, MonitoredProductServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-203397", e.Message));
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
            var productGetDtos = _mapper.Map<List<MonitoredProductGetDto>>(products);
            result.SetData(productGetDtos, page, pageSize, MonitoredProductServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-304796", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<MonitoredProductGetDto>> GetByUserIdAsync(string userId)
    {
        var result = new ServiceCollectionResult<MonitoredProductGetDto>();

        try
        {
            BusinessRules.Run(("MNPR-950070", BusinessRules.CheckId(userId)));

            var products = await _monitoredProductDal.GetAllAsync(b => b.OwnerId.ToString().Equals(userId));
            var productGetDtos = _mapper.Map<List<MonitoredProductGetDto>>(products);
            result.SetData(productGetDtos, successMessage: MonitoredProductServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-304796", e.Message));
        }

        return result;
    }

    [ValidationAspect(typeof(MonitoredProductAddValidator))]
    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> AddAsync(MonitoredProductAddDto productAddDto)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(("MNPR-950070", BusinessRules.CheckDtoNull(productAddDto)));
            var product = _mapper.Map<MonitoredProduct>(productAddDto);
            await _monitoredProductDal.AddAsync(product);
            result.SetData(_mapper.Map<MonitoredProductGetDto>(product), MonitoredProductServiceMessages.Added);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-203397", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> DeleteByIdAsync(string id)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(("MNPR-950070", BusinessRules.CheckId(id)));
            var product = await _monitoredProductDal.GetAsync(b => b.Id.ToString().Equals(id));
            BusinessRules.Run(("MNPR-759000", BusinessRules.CheckEntityNull(product)));

            await _monitoredProductDal.SoftDeleteAsync(product!);
            result.SetData(_mapper.Map<MonitoredProductGetDto>(product), MonitoredProductServiceMessages.Deleted);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("MNPR-661359", e.Message));
        }

        return result;
    }

    [ValidationAspect(typeof(MonitoredProductUpdateValidator))]
    public async Task<ServiceObjectResult<MonitoredProductGetDto?>> UpdateAsync(
        MonitoredProductUpdateDto productUpdateDto)
    {
        var result = new ServiceObjectResult<MonitoredProductGetDto?>();

        try
        {
            BusinessRules.Run(
                ("MNPR-950070", BusinessRules.CheckDtoNull(productUpdateDto)),
                ("MNPR-950070", BusinessRules.CheckId(productUpdateDto.Id.ToString()))
            );

            var product = await _monitoredProductDal.GetAsync(b => b.Id.ToString().Equals(productUpdateDto.Id.ToString()));
            BusinessRules.Run(("MNPR-759000", BusinessRules.CheckEntityNull(product)));

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
            result.Fail(new ErrorMessage("MNPR-203397", e.Message));
        }

        return result;
    }
}
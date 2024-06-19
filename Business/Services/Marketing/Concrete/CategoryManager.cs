using AutoMapper;
using Business.Constants.Messages.Services.Marketing;
using Business.Services.Marketing.Abstract;
using Core.ExceptionHandling;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Marketing;
using Domain.DTOs.Marketing;

namespace Business.Services.Marketing.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal = ServiceTool.GetService<ICategoryDal>()!;
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;

    public async Task<ServiceObjectResult<CategoryGetDto?>> GetByIdAsync(Guid id)
    {
        var result = new ServiceObjectResult<CategoryGetDto?>();

        try
        {
            var category = await _categoryDal.GetAsync(b => b.Id.Equals(id));
            BusinessRules.Run(("CTGR-445683", BusinessRules.CheckEntityNull(category)));

            var categoryGetDto = _mapper.Map<CategoryGetDto>(category);
            result.SetData(categoryGetDto, CategoryServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("RPRT-183243", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<CategoryGetDto?>> GetAllAsync()
    {
        var result = new ServiceCollectionResult<CategoryGetDto?>();

        try
        {
            var categories = await _categoryDal.GetAllAsync();
            var categoryGetDtos = _mapper.Map<List<CategoryGetDto>>(categories);
            result.SetData(categoryGetDtos, successMessage: CategoryServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CTGR-183243", e.Message));
        }

        return result;
    }
}
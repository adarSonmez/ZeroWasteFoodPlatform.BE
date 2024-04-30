using AutoMapper;
using Business.Constants.Messages.Services.Membership;
using Business.Services.Membership.Abstract;
using Core.ExceptionHandling;
using Core.Extensions;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Membership;
using Domain.DTOs.Membership;
using Domain.FilterModels.Membership;

namespace Business.Services.Membership.Concrete;

public class BusinessManager : IBusinessService
{
    private readonly IBusinessDal _businessDal = ServiceTool.GetService<IBusinessDal>()!;
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;

    public async Task<ServiceObjectResult<BusinessGetDto?>> GetByIdAsync(string id)
    {
        var result = new ServiceObjectResult<BusinessGetDto?>();

        try
        {
            BusinessRules.Run(("BSNS-508769", BusinessRules.CheckId(id)));

            var business = await _businessDal.GetAsync(b => id.Equals(b.Id.ToString()));
            BusinessRules.Run(("BSNS-386849", BusinessRules.CheckEntityNull(business)));

            var businessGetDto = _mapper.Map<BusinessGetDto>(business);
            result.SetData(businessGetDto, BusinessServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("BSNS-723931", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<BusinessGetDto?>> GetByUsernameAsync(string username)
    {
        var result = new ServiceObjectResult<BusinessGetDto?>();

        try
        {
            BusinessRules.Run(("BSNS-871185", BusinessRules.CheckStringNullOrEmpty(username)));

            var business = await _businessDal.GetAsync(b => username.Equals(b.Username));
            BusinessRules.Run(("BSNS-707911", BusinessRules.CheckEntityNull(business)));

            var businessGetDto = _mapper.Map<BusinessGetDto>(business);
            result.SetData(businessGetDto, BusinessServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("BSNS-433141", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<BusinessGetDto?>> GetByEmailAsync(string email)
    {
        var result = new ServiceObjectResult<BusinessGetDto?>();

        try
        {
            BusinessRules.Run(("BSNS-414121", BusinessRules.CheckEmail(email)));

            var business = await _businessDal.GetAsync(b => email.Equals(b.Email));
            BusinessRules.Run(("BSNS-570215", BusinessRules.CheckEntityNull(business)));

            var businessGetDto = _mapper.Map<BusinessGetDto>(business);
            result.SetData(businessGetDto, BusinessServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("BSNS-128549", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<BusinessGetDto>> GetListAsync(BusinessFilterModel? filterModel, int page,
        int pageSize)
    {
        var result = new ServiceCollectionResult<BusinessGetDto>();

        try
        {
            var filters = filterModel?.ToExpression();
            var businesses = await _businessDal.GetAllAsync(filters);
            var businessGetDtos = _mapper.Map<List<BusinessGetDto>>(businesses);
            result.SetData(businessGetDtos, page, pageSize, BusinessServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("BSNS-553426", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<BusinessGetDto?>> UpdateAsync(BusinessUpdateDto businessUpdateDto)
    {
        var result = new ServiceObjectResult<BusinessGetDto?>();

        try
        {
            BusinessRules.Run(
                ("BSNS-257397", BusinessRules.CheckDtoNull(businessUpdateDto)),
                ("BSNS-445608", await CheckIfUsernameExists(businessUpdateDto.Username, true)),
                ("BSNS-138922", await CheckIfEmailExists(businessUpdateDto.Email, true))
            );

            var businessToUpdate =
                await _businessDal.GetAsync(b => businessUpdateDto.Id.ToString().Equals(b.Id.ToString()));
            BusinessRules.Run(("BSNS-257397", BusinessRules.CheckEntityNull(businessToUpdate)));

            businessToUpdate = _mapper.Map(businessUpdateDto, businessToUpdate)!;
            await _businessDal.UpdateAsync(businessToUpdate);

            var updatedBusiness =
                await _businessDal.GetAsync(b => businessUpdateDto.Id.ToString().Equals(b.Id.ToString()));
            var businessGetDto = _mapper.Map<BusinessGetDto>(updatedBusiness);
            result.SetData(businessGetDto, BusinessServiceMessages.Updated);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("BSNS-569486", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<BusinessGetDto?>> DeleteByIdAsync(string id)
    {
        var result = new ServiceObjectResult<BusinessGetDto?>();

        try
        {
            BusinessRules.Run(("BSNS-160835", BusinessRules.CheckId(id)));

            var business = await _businessDal.GetAsync(b => id.Equals(b.Id.ToString()));
            BusinessRules.Run(("BSNS-188217", BusinessRules.CheckEntityNull(business)));

            await _businessDal.SoftDeleteAsync(business!);
            result.SetData(_mapper.Map<BusinessGetDto>(business), BusinessServiceMessages.Deleted);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("BSNS-723931", e.Message));
        }

        return result;
    }

    private async Task<string?> CheckIfUsernameExists(string? username, bool isUpdate = false)
    {
        if (!isUpdate)
        {
            BusinessRules.Run(("BSNS-452033", BusinessRules.CheckStringNullOrEmpty(username)));
        }
        else
        {
            if (string.IsNullOrEmpty(username))
                return null;
        }

        var business = await _businessDal.GetAsync(b => username!.Equals(b.Username));

        return business is not null ? BusinessServiceMessages.UsernameAlreadyExists : null;
    }

    private async Task<string?> CheckIfEmailExists(string? email, bool isUpdate = false)
    {
        if (!isUpdate)
        {
            BusinessRules.Run(("BSNS-165394", BusinessRules.CheckEmail(email!)));
        }
        else
        {
            if (string.IsNullOrEmpty(email))
                return null;
        }

        var business = await _businessDal.GetAsync(b => email!.Equals(b.Email));

        return business is not null ? BusinessServiceMessages.EmailAlreadyExists : null;
    }
}
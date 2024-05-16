using AutoMapper;
using Business.Constants.Messages.Services.Analytics;
using Business.Services.Analytics.Abstract;
using Core.ExceptionHandling;
using Core.Extensions;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Analytics;
using Domain.DTOs.Analytics;
using Domain.FilterModels.Analytics;

namespace Business.Services.Analytics.Concrete;

public class ReportManager : IReportService
{
    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly IReportDal _reportDal = ServiceTool.GetService<IReportDal>()!;

    public async Task<ServiceObjectResult<ReportGetDto?>> GetByIdAsync(Guid id)
    {
        var result = new ServiceObjectResult<ReportGetDto?>();

        try
        {
            var report = await _reportDal.GetAsync(b => b.Id.Equals(id));
            BusinessRules.Run(("RPRT-786267", BusinessRules.CheckEntityNull(report)));

            var reportGetDto = _mapper.Map<ReportGetDto>(report);
            result.SetData(reportGetDto, ReportServiceMessages.Retrieved);
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

    public async Task<ServiceCollectionResult<ReportGetDto>> GetListAsync(ReportFilterModel? filterModel, int page,
        int pageSize)
    {
        var result = new ServiceCollectionResult<ReportGetDto>();

        try
        {
            var filters = filterModel?.ToExpression();
            var reports = await _reportDal.GetAllAsync(filters);
            var reportGetDtos = _mapper.Map<List<ReportGetDto>>(reports);
            result.SetData(reportGetDtos, page, pageSize, ReportServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("RPRT-123147", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<ReportGetDto?>> DeleteByIdAsync(Guid id)
    {
        var result = new ServiceObjectResult<ReportGetDto?>();

        try
        {
            var report = await _reportDal.GetAsync(b => b.Id.Equals(id));
            BusinessRules.Run(("RPRT-516247", BusinessRules.CheckEntityNull(report)));

            await _reportDal.SoftDeleteAsync(report!);
            result.SetData(_mapper.Map<ReportGetDto>(report), ReportServiceMessages.Deleted);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("RPRT-595739", e.Message));
        }

        return result;
    }
}
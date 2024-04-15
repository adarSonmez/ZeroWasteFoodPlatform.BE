using AutoMapper;
using Domain.DTOs.Analytics;
using Domain.Entities.Analytics;

namespace Domain.EntityMapping.AutoMapper.Analytics;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Report, ReportGetDto>();
    }
}
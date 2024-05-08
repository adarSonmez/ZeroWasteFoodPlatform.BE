using AutoMapper;
using Domain.DTOs.Marketing;
using Domain.Entities.Marketing;

namespace Domain.EntityMapping.AutoMapper.Marketing;

public class MonitoredProductProfile : Profile
{
    public MonitoredProductProfile()
    {
        CreateMap<MonitoredProduct, MonitoredProductGetDto>();
        CreateMap<MonitoredProductAddDto, MonitoredProduct>();

        CreateMap<MonitoredProductUpdateDto, MonitoredProduct>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<MonitoredProduct, ProductGetDto>();
    }
}
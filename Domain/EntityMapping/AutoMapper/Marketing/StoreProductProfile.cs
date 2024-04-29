using AutoMapper;
using Domain.DTOs.Marketing;
using Domain.Entities.Marketing;

namespace Domain.EntityMapping.AutoMapper.Marketing;

public class StoreProductProfile : Profile
{
    public StoreProductProfile()
    {
        CreateMap<StoreProduct, StoreProductGetDto>();
        CreateMap<StoreProductAddDto, StoreProduct>();

        CreateMap<StoreProductUpdateDto, StoreProduct>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
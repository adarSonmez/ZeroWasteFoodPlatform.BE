using AutoMapper;
using Domain.DTOs.Membership;
using Domain.Entities.Membership;

namespace Domain.EntityMapping.AutoMapper.Membership;

public class BusinessProfile : Profile
{
    public BusinessProfile()
    {
        CreateMap<Business, BusinessGetDto>();

        CreateMap<BusinessUpdateDto, Business>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
using AutoMapper;
using Domain.DTOs.Authentication;
using Domain.DTOs.Membership;
using Domain.Entities.Membership;

namespace Domain.EntityMapping.AutoMapper.Membership;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerGetDto>();

        CreateMap<CustomerUpdateDto, Customer>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CustomerRegisterDto, Customer>();
    }
}
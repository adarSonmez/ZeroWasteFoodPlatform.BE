using AutoMapper;
using Domain.DTOs.Marketing;
using Domain.Entities.Marketing;

namespace Domain.EntityMapping.AutoMapper.Marketing;

public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryGetDto>();
    }
}
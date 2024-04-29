using Core.Services;
using Domain.Entities.Marketing;

namespace Domain.FilterModels.Marketing;

// TODO: Add filtering by price
public class StoreProductFilterModel : IServiceFilterModel<StoreProduct>
{
    public string? NameQuery { get; set; } = null;
}
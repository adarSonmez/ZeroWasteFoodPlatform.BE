using Core.Services;
using Domain.Entities.Marketing;

namespace Domain.FilterModels.Marketing;

public sealed class StoreProductFilterModel : IServiceFilterModel<StoreProduct>
{
    public string? NameQuery { get; set; } = null;
}
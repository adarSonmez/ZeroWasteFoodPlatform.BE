using Core.Services;
using Domain.Entities.Marketing;

namespace Domain.FilterModels.Marketing;

public sealed class MonitoredProductFilterModel : IServiceFilterModel<MonitoredProduct>
{
    public string? NameQuery { get; set; } = null;
}
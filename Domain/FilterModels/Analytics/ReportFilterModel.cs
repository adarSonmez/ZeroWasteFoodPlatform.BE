using Core.Services;
using Domain.Entities.Analytics;

namespace Domain.FilterModels.Analytics;

// TODO: Add filtering by date and amount
public class ReportFilterModel : IServiceFilterModel<Report>
{
    public string? ReportNameQuery { get; set; } = null;
    public string? ProductNameQuery { get; set; } = null;
    public string? ManufacturerQuery { get; set; } = null;
    public string? LocationQuery { get; set; } = null;
}
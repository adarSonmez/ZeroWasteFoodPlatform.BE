using Core.Domain.Abstract;

namespace Domain.DTOs.Analytics;

public class ReportGetDto : IDto
{
    public Guid Id { get; set; } = default!;

    public string ReportName { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime StartDate { get; set; } = default!;

    public DateTime EndDate { get; set; } = default!;

    public int SuppliedAmount { get; set; } = default!;

    public int SoldAmount { get; set; } = default!;
}
using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstract;

namespace Domain.Entities.Analytics;

public class Report : EntityBase
{
    [StringLength(127)] public string ReportName { get; set; } = default!;

    public string Content { get; set; } = default!;

    [StringLength(127)] public string ProductName { get; set; } = default!;

    [StringLength(255)] public string Manufacturer { get; set; } = default!;

    [StringLength(255)] public string Location { get; set; } = default!;

    // Analysis start date
    public DateTime StartDate { get; set; } = DateTime.Now;

    // Analysis end date
    public DateTime EndDate { get; set; } = DateTime.Now;

    public int SuppliedAmount { get; set; }

    public int SoldAmount { get; set; }
}
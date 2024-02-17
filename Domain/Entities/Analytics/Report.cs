using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Abstract;

namespace Domain.Entities.Analytics;

[Table("Reports", Schema = "Analytics")]
public class Report : IEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

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
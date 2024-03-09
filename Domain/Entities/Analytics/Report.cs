using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Abstract;

namespace Domain.Entities.Analytics;

[Table("Reports", Schema = "Analytics")]
public class Report : IEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(127)] public string ReportName { get; set; } = null!;

    public string Content { get; set; } = null!;

    [StringLength(127)] public string ProductName { get; set; } = null!;

    [StringLength(255)] public string Manufacturer { get; set; } = null!;

    [StringLength(255)] public string Location { get; set; } = null!;

    // Analysis start date
    public DateTime StartDate { get; set; } = DateTime.Now;

    // Analysis end date
    public DateTime EndDate { get; set; } = DateTime.Now;

    public int SuppliedAmount { get; set; }

    public int SoldAmount { get; set; }
}
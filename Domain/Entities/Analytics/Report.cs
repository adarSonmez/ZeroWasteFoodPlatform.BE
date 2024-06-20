using System.ComponentModel.DataAnnotations;
using Core.Domain.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Analytics;

public class Report : IEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(127)] [Unicode] public string ReportName { get; set; } = null!;

    [StringLength(65535)] [Unicode] public string Content { get; set; } = null!;

    [StringLength(127)] [Unicode] public string ProductName { get; set; } = null!;

    [StringLength(255)] [Unicode] public string Manufacturer { get; set; } = null!;

    [StringLength(255)] [Unicode] public string Location { get; set; } = null!;

    // Analysis start date
    public DateTime StartDate { get; set; } = DateTime.Now;

    // Analysis end date
    public DateTime EndDate { get; set; } = DateTime.Now;

    public int SuppliedAmount { get; set; }

    public int SoldAmount { get; set; }

    // [Timestamp]
    [Comment("The version of the row. Used for concurrency.")]
    public byte[]? RowVersion { get; set; }
}
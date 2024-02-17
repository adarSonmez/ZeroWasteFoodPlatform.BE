using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Membership;

namespace Domain.Entities.Marketing;

[Table("StoreProducts", Schema = "Marketing")]
public class StoreProduct : Product
{
    public decimal OriginalPrice { get; set; } = 0;

    public new string Photo { get; set; } = default!;

    public double PercentDiscount { get; set; } = 0;

    // Foreign Key
    public Guid BusinessId { get; set; }

    public virtual Business Business { get; set; } = default!;
}
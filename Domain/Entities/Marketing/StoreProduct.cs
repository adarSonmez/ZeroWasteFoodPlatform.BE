using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Membership;

namespace Domain.Entities.Marketing;

[Table("StoreProducts", Schema = "Marketing")]
public class StoreProduct : Product
{
    public decimal OriginalPrice { get; set; }

    public double PercentDiscount { get; set; }

    // Foreign Key
    public Guid BusinessId { get; set; }

    public virtual Business Business { get; set; } = default!;
}
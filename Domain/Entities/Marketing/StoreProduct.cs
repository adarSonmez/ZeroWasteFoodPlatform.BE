using Domain.Entities.Association;
using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Marketing;

// Derived entity from Product.cs
public class StoreProduct : Product
{
    [Precision(18, 2)] public decimal OriginalPrice { get; set; }

    public double PercentDiscount { get; set; }

    public Guid BusinessId { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<CustomerStoreProduct> InterestedCustomers { get; set; } =
        new List<CustomerStoreProduct>();
}
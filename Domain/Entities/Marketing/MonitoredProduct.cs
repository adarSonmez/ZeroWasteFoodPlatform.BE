using Domain.Entities.Membership;

namespace Domain.Entities.Marketing;

// Derived entity from Product.cs
public class MonitoredProduct : Product
{
    public Guid OwnerId { get; set; }

    public virtual User Owner { get; set; } = null!;
}
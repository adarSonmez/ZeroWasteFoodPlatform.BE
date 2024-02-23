namespace Domain.Entities.Marketing;

// Derived entity from Product.cs
public class MonitoredProduct : Product
{
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
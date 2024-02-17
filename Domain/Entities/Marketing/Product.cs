using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstract;

namespace Domain.Entities.Marketing;

public abstract class Product : EntityBase
{
    [StringLength(127)] public string Name { get; set; } = default!;

    [StringLength(1023)] public string Description { get; set; } = default!;

    [StringLength(2047)]
    public string? Photo { get; set; } = "https://www.4me.com/wp-content/uploads/2018/01/4me-icon-product.png";

    public DateTime ExpirationDate { get; set; } = DateTime.Now;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
using System.ComponentModel.DataAnnotations;
using Core.Domain.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Marketing;

public class Product : EntityBase
{
    [StringLength(127)] [Unicode] public string Name { get; set; } = null!;

    [StringLength(1023)] [Unicode] public string Description { get; set; } = null!;

    [StringLength(2047)]
    public string? Photo { get; set; } = "https://www.4me.com/wp-content/uploads/2018/01/4me-icon-product.png";

    public DateTime ExpirationDate { get; set; } = DateTime.Now;

    [StringLength(127)] public string Barcode { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
}
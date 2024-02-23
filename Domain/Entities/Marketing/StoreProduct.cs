using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Abstract;
using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Marketing;

[Table("StoreProducts", Schema = "Marketing")]
public class StoreProduct : IEntity
{
    [Key] public Guid ProductId { get; set; }

    [Precision(18, 2)] public decimal OriginalPrice { get; set; }

    public double PercentDiscount { get; set; }

    public Guid BusinessId { get; set; }

    public virtual Business Business { get; set; } = null!;


    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
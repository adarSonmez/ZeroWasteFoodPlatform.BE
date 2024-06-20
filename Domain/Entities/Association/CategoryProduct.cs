using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Abstract;
using Domain.Entities.Marketing;

namespace Domain.Entities.Association;

public class CategoryProduct : IEntity
{
    public Guid CategoryId { get; set; }

    public Guid ProductId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
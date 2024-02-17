using Core.Entities.Abstract;
using Domain.Entities.Marketing;

namespace Domain.Entities.Association;

public class CategoryProduct : IEntity
{
    // Foreign Key
    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = default!;

    // Foreign Key
    public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = default!;
}
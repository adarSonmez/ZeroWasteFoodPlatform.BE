using Core.Entities.Abstract;

namespace Domain.Entities.Marketing;

public class Category : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
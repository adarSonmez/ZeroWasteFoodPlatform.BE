using System.ComponentModel.DataAnnotations;
using Core.Domain.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Marketing;

public class Category : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(63)] [Unicode] public string Name { get; set; } = null!;

    [StringLength(2047)] [Unicode] public string? Description { get; set; }

    [StringLength(1023)]
    public string Photo { get; set; } =
        "https://cdn3.iconfinder.com/data/icons/glypho-social-and-other-logos/64/logo-share-512.png";

    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
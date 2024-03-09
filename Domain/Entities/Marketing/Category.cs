using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Abstract;

namespace Domain.Entities.Marketing;

[Table("Categories", Schema = "Marketing")]
public class Category : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(63)] public string Name { get; set; } = null!;

    [StringLength(2047)] public string? Description { get; set; }

    [StringLength(1023)]
    public string Photo { get; set; } =
        "https://cdn3.iconfinder.com/data/icons/glypho-social-and-other-logos/64/logo-share-512.png";

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
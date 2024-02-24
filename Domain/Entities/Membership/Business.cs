using System.ComponentModel.DataAnnotations;
using Domain.Entities.Marketing;

namespace Domain.Entities.Membership;

// Derived entity from User.cs
public class Business : User
{
    [StringLength(1023)] public string Address { get; set; } = null!;

    [StringLength(127)] public string Name { get; set; } = null!;

    [StringLength(127)] public string? Website { get; set; }

    [StringLength(2047)] public string? Description { get; set; }

    [StringLength(1023)]
    public string Logo { get; set; } = "https://png.pngtree.com/element_our/sm/20180418/sm_5ad74b2cb4473.jpg";

    [StringLength(1023)] public string? CoverPhoto { get; set; }

    public virtual ICollection<StoreProduct> Products { get; set; } = new List<StoreProduct>();
}
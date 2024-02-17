using System.ComponentModel.DataAnnotations;
using Core.Constants;
using Domain.Entities.Marketing;

namespace Domain.Entities.Membership;

public class Business : User
{
    [StringLength(1023)] public string Address { get; set; } = default!;

    [StringLength(127)] public string Name { get; set; } = default!;

    [StringLength(127)] public string? Website { get; set; }

    public string? Description { get; set; }

    public string? Logo { get; set; }

    public string? CoverPhoto { get; set; }

    [StringLength(15)] public new string Role { get; set; } = UserRoles.Business;

    public virtual ICollection<StoreProduct> Products { get; set; } = new List<StoreProduct>();
}
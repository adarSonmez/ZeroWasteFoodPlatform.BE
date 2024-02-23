using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Constants;
using Core.Entities.Abstract;
using Domain.Entities.Marketing;

namespace Domain.Entities.Membership;

[Table("Businesses", Schema = "Membership")]
public class Business : IEntity
{
    [Key] public Guid UserId { get; set; }

    [StringLength(1023)] public string Address { get; set; } = null!;

    [StringLength(127)] public string Name { get; set; } = null!;

    [StringLength(127)] public string? Website { get; set; }

    [StringLength(2047)] public string? Description { get; set; }

    [StringLength(1023)]
    public string Logo { get; set; } = "https://png.pngtree.com/element_our/sm/20180418/sm_5ad74b2cb4473.jpg";

    [StringLength(1023)] public string? CoverPhoto { get; set; }

    [StringLength(15)] public string Role { get; set; } = UserRoles.Business;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<StoreProduct> Products { get; set; } = new List<StoreProduct>();
}
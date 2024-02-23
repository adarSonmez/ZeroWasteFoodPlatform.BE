using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Constants;
using Core.Entities.Abstract;

namespace Domain.Entities.Membership;

[Table("Customers", Schema = "Membership")]
public class Customer : IEntity
{
    [Key] public Guid UserId { get; set; }

    [StringLength(127)] public string FirstName { get; set; } = null!;

    [StringLength(127)] public string LastName { get; set; } = null!;

    [StringLength(15)] public string Role { get; set; } = UserRoles.Customer;

    [StringLength(1023)]
    public string Avatar { get; set; } = "https://static.zooniverse.org/www.zooniverse.org/assets/simple-avatar.png";

    public virtual User User { get; set; } = null!;
}
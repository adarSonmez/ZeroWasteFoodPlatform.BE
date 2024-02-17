using System.ComponentModel.DataAnnotations;
using Core.Constants;

namespace Domain.Entities.Membership;

public class Customer : User
{
    [StringLength(127)] public string FirstName { get; set; } = default!;

    [StringLength(127)] public string LastName { get; set; } = default!;

    [StringLength(15)] public string Role { get; set; } = UserRoles.Customer;
}
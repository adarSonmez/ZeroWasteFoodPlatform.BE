using System.ComponentModel.DataAnnotations;
using Core.Domain.Abstract;
using Domain.Entities.Marketing;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Membership;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(PhoneNumber), IsUnique = true)]
public class User : EntityBase
{
    [StringLength(127)] public string Username { get; set; } = null!;

    [StringLength(127)] public string Email { get; set; } = null!;

    [StringLength(127)] public string PhoneNumber { get; set; } = null!;

    [StringLength(6)] public string? LoginVerificationCode { get; set; }

    public string? ActiveToken { get; set; }

    public DateTime? LoginVerificationCodeExpiration { get; set; }

    public byte[] PasswordSalt { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public bool UseMultiFactorAuthentication { get; set; }

    public bool EmailVerified { get; set; }

    public bool PhoneNumberVerified { get; set; }

    public DateTime LastLoginTime { get; set; }

    [StringLength(15)] public string Role { get; set; } = null!;

    public virtual ICollection<MonitoredProduct> MonitoredProduct { get; set; } = new HashSet<MonitoredProduct>();
}
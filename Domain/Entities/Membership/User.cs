using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Abstract;
using Domain.Entities.Marketing;

namespace Domain.Entities.Membership;

[Table("Users", Schema = "Membership")]
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

    public virtual ICollection<MonitoredProduct> MonitoredProduct { get; set; } = new List<MonitoredProduct>();
}
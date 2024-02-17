using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstract;

namespace Domain.Entities.Membership;

public abstract class User : EntityBase
{
    [StringLength(127)] public string Username { get; set; } = default!;

    [StringLength(127)] public string Email { get; set; } = default!;

    [StringLength(127)] public string PhoneNumber { get; set; } = default!;

    [StringLength(6)] public string? LoginVerificationCode { get; set; }

    public DateTime? LoginVerificationCodeExpiration { get; set; }

    public byte[] PasswordSalt { get; set; } = default!;

    public byte[] PasswordHash { get; set; } = default!;

    public DateTime LastLoginTime { get; set; }

    [StringLength(15)] public string Role { get; set; } = default!;
}
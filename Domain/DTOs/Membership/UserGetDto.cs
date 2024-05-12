using Core.Domain.Abstract;

namespace Domain.DTOs.Membership;

public class UserGetDto : IDto
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string UseMultiFactorAuthentication { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool EmailVerified { get; set; }

    public bool PhoneNumberVerified { get; set; }

    public string LastLoginTime { get; set; } = null!;
}
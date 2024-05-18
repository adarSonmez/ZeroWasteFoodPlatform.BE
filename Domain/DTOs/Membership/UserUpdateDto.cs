using Core.Domain.Abstract;

namespace Domain.DTOs.Membership;

public class UserUpdateDto : IDto
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? UseMultiFactorAuthentication { get; set; }
}
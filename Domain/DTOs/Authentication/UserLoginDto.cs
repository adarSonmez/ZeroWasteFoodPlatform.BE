using Core.Domain.Abstract;

namespace Domain.DTOs.Authentication;

public class UserLoginDto : IDto
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string Password { get; set; } = null!;
}
using Core.Domain.Abstract;

namespace Domain.DTOs.Authentication;

public class VerifyEmailCodeDto : IDto
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}
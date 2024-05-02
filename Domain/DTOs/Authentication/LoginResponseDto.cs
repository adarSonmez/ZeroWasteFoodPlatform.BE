using Core.Domain.Abstract;
using Core.Security.SessionManagement;
using Domain.DTOs.Membership;

namespace Domain.DTOs.Authentication;

public class LoginResponseDto : IDto
{
    public UserGetDto User { get; set; } = null!;
    public Token Token { get; set; } = null!;
}
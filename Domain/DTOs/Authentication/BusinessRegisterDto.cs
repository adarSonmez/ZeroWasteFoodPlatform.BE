using Core.Domain.Abstract;

namespace Domain.DTOs.Authentication;

// TODO: Verify email and phone number
public class BusinessRegisterDto : IDto
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Website { get; set; }
    public string? Description { get; set; }
    public string? Logo { get; set; } = "https://png.pngtree.com/element_our/sm/20180418/sm_5ad74b2cb4473.jpg";
    public string? CoverPhoto { get; set; }
}
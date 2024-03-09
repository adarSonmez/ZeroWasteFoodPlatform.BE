namespace Domain.DTOs.Membership;

public class BusinessUpdateDto : UserUpdateDto
{
    public string? Address { get; set; }

    public string? Name { get; set; }

    public string? Website { get; set; }

    public string? Description { get; set; }

    public string? Logo { get; set; }

    public string? CoverPhoto { get; set; }
}
namespace Domain.DTOs.Membership;

public class BusinessGetDto : UserGetDto
{
    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string CoverPhoto { get; set; } = null!;
}
namespace Domain.DTOs.Membership;

public class CustomerGetDto : UserGetDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Avatar { get; set; } = null!;
}
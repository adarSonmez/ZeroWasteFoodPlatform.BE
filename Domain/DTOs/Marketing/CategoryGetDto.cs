using Core.Domain.Abstract;

namespace Domain.DTOs.Marketing;

public class CategoryGetDto : IDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Photo { get; set; } = null!;
}
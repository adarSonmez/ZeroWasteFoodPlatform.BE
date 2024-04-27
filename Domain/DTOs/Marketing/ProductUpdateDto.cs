using Core.Domain.Abstract;

namespace Domain.DTOs.Marketing;

public class ProductUpdateDto : IDto
{
    public Guid Id { get; set; } = default!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public ICollection<string>? CategoryIds { get; set; }
}
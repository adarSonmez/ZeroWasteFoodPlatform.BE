using Core.Domain.Abstract;

namespace Domain.DTOs.Marketing;

public class ProductGetDto : IDto
{
    public Guid Id { get; set; } = default!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public DateTime? ExpirationDate { get; set; } = default!;

    public virtual ICollection<CategoryGetDto> Categories { get; set; } = default!;
}
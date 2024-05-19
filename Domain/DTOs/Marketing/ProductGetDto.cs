using Core.Domain.Abstract;

namespace Domain.DTOs.Marketing;

public class ProductGetDto : IDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public DateTime? ExpirationDate { get; set; }

    public string Barcode { get; set; } = null!;

    public virtual ICollection<CategoryGetDto> Categories { get; set; }
}
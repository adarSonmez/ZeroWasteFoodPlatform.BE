using Core.Domain.Abstract;

namespace Domain.DTOs.Marketing;

public class ProductAddDto : IDto
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Photo { get; set; } = "https://www.4me.com/wp-content/uploads/2018/01/4me-icon-product.png";

    public DateTime ExpirationDate { get; set; }

    public string Barcode { get; set; } = null!;

    public ICollection<string> CategoriesIds { get; set; } = new List<string>();
}
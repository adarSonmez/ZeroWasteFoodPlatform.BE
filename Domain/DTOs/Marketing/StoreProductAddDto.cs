namespace Domain.DTOs.Marketing;

public class StoreProductAddDto : ProductAddDto
{
    public decimal OriginalPrice { get; set; } = default!;

    public double PercentDiscount { get; set; } = default!;
}
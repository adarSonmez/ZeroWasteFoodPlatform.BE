namespace Domain.DTOs.Marketing;

public class StoreProductAddDto : ProductAddDto
{
    public decimal OriginalPrice { get; set; }

    public double PercentDiscount { get; set; }
}
namespace Domain.DTOs.Marketing;

public class StoreProductUpdateDto : ProductUpdateDto
{
    public decimal? OriginalPrice { get; set; }

    public double? PercentDiscount { get; set; }
}
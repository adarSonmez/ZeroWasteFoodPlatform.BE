using Domain.DTOs.Membership;

namespace Domain.DTOs.Marketing;

public class StoreProductGetDto : ProductGetDto
{
    public decimal OriginalPrice { get; set; }

    public double PercentDiscount { get; set; }

    public virtual BusinessGetDto Business { get; set; } = null!;
}
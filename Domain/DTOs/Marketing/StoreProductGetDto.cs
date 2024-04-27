using Domain.DTOs.Membership;

namespace Domain.DTOs.Marketing;

public class StoreProductGetDto : ProductGetDto
{
    public decimal OriginalPrice { get; set; } = default!;

    public double PercentDiscount { get; set; } = default!;

    public virtual BusinessGetDto Business { get; set; } = null!;
}
using Core.Domain.Abstract;

namespace Domain.DTOs.Marketing;

public class StoreProductManipulateShoppingListDto : IDto
{
    public Guid ProductId { get; set; }
}
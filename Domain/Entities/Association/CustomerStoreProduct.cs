using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Abstract;
using Domain.Entities.Marketing;
using Domain.Entities.Membership;

namespace Domain.Entities.Association;

public class CustomerStoreProduct : IEntity
{
    public Guid CustomerId { get; set; }

    public Guid ProductId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual StoreProduct Product { get; set; } = null!;
}
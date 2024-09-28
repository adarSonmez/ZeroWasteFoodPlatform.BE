using Core.DataAccess.Abstract;
using Domain.Entities.Association;

namespace DataAccess.Repositories.Abstract.Association;

/// <summary>
/// Represents the data access layer interface for the CustomerStoreProduct entity.
/// </summary>
public interface ICustomerStoreProductDal : IEntityRepository<CustomerStoreProduct>
{
}
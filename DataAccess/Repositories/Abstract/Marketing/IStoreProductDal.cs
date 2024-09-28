using Core.DataAccess.Abstract;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Abstract.Marketing;

/// <summary>
/// Represents a data access layer interface for the StoreProduct entity.
/// </summary>
public interface IStoreProductDal : IEntityRepository<StoreProduct>
{
}
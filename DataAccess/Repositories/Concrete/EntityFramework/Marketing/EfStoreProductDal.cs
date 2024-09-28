using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Marketing;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Concrete.EntityFramework.Marketing;

/// <summary>
/// Represents the concrete implementation of the Entity Framework repository for StoreProduct entities.
/// </summary>
public class EfStoreProductDal : EfEntityRepository<StoreProduct, EfDbContext>, IStoreProductDal
{
}
using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Marketing;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Concrete.EntityFramework.Marketing;

public class EfStoreProductDal : EfEntityRepository<StoreProduct, EfDbContext>, IStoreProductDal
{
}
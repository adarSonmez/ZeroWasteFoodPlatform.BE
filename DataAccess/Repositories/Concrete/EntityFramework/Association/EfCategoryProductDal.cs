using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Association;
using Domain.Entities.Association;

namespace DataAccess.Repositories.Concrete.EntityFramework.Association;

/// <summary>
/// Represents the concrete implementation of the ICategoryProductDal interface using Entity Framework.
/// </summary>
public class EfCategoryProductDal : EfEntityRepository<CategoryProduct, EfDbContext>, ICategoryProductDal
{
}
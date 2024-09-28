using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Marketing;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Concrete.EntityFramework.Marketing;

/// <summary>
/// Represents the concrete implementation of the category data access layer using Entity Framework.
/// </summary>
public class EfCategoryDal : EfEntityRepository<Category, EfDbContext>, ICategoryDal
{
}
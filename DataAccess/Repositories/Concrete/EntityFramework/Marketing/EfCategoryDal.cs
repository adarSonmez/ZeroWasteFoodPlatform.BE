using Core.DataAccess.EntityFramework;
using DataAccess.Repositories.Abstract.Marketing;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Concrete.EntityFramework.Marketing;

public class EfCategoryDal : EfEntityRepository<Category>, ICategoryDal
{
}
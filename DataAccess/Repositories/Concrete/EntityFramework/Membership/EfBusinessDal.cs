using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Membership;
using Domain.Entities.Membership;

namespace DataAccess.Repositories.Concrete.EntityFramework.Membership;

public class EfBusinessDal : EfEntityRepository<Business, EfDbContext>, IBusinessDal
{
}
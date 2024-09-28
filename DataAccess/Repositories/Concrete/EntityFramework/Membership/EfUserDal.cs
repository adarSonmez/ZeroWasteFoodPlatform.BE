using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Membership;
using Domain.Entities.Membership;

namespace DataAccess.Repositories.Concrete.EntityFramework.Membership;

/// <summary>
/// Represents the concrete implementation of the IUserDal interface using Entity Framework.
/// </summary>
public class EfUserDal : EfEntityRepository<User, EfDbContext>, IUserDal
{
}
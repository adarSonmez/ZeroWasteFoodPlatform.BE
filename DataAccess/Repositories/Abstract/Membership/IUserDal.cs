using Core.DataAccess.Abstract;
using Domain.Entities.Membership;

namespace DataAccess.Repositories.Abstract.Membership;

public interface IUserDal : IEntityRepository<User>
{
}
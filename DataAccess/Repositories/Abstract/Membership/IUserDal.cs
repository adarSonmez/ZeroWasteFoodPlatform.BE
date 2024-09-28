using Core.DataAccess.Abstract;
using Domain.Entities.Membership;

namespace DataAccess.Repositories.Abstract.Membership;

/// <summary>
/// Represents the data access layer interface for the User entity.
/// </summary>
public interface IUserDal : IEntityRepository<User>
{
}
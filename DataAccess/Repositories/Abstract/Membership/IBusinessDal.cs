using Core.DataAccess.Abstract;
using Domain.Entities.Membership;

namespace DataAccess.Repositories.Abstract.Membership;

/// <summary>
/// Represents the data access layer interface for the Business entity.
/// </summary>
public interface IBusinessDal : IEntityRepository<Business>
{
}
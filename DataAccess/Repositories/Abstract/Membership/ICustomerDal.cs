using Core.DataAccess.Abstract;
using Domain.Entities.Membership;

namespace DataAccess.Repositories.Abstract.Membership;

/// <summary>
/// Represents a data access layer interface for the Customer entity.
/// </summary>
public interface ICustomerDal : IEntityRepository<Customer>
{
}
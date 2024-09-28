using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Membership;
using Domain.Entities.Membership;

namespace DataAccess.Repositories.Concrete.EntityFramework.Membership;

/// <summary>
/// Represents the concrete implementation of the customer data access layer using Entity Framework.
/// </summary>
public class EfCustomerDal : EfEntityRepository<Customer, EfDbContext>, ICustomerDal
{
}
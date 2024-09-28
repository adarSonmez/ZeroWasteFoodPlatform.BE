using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Analytics;
using Domain.Entities.Analytics;

namespace DataAccess.Repositories.Concrete.EntityFramework.Analytics;

/// <summary>
/// Represents the concrete implementation of the IReportDal interface using Entity Framework.
/// </summary>
public class EfReportDal : EfEntityRepository<Report, EfDbContext>, IReportDal
{
}
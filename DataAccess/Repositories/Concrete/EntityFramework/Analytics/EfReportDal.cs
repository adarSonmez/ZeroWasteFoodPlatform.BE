using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Analytics;
using Domain.Entities.Analytics;

namespace DataAccess.Repositories.Concrete.EntityFramework.Analytics;

public class EfReportDal : EfEntityRepository<Report, EfEfDbContext>, IReportDal
{
}
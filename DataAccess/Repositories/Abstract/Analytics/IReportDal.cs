using Core.DataAccess.Abstract;
using Domain.Entities.Analytics;

namespace DataAccess.Repositories.Abstract.Analytics;

/// <summary>
/// Represents the data access layer interface for the Report entity.
/// </summary>
public interface IReportDal : IEntityRepository<Report>
{
}
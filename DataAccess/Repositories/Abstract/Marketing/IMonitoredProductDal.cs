using Core.DataAccess.Abstract;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Abstract.Marketing;

/// <summary>
/// Represents the data access layer interface for the MonitoredProduct entity.
/// </summary>
public interface IMonitoredProductDal : IEntityRepository<MonitoredProduct>
{
}
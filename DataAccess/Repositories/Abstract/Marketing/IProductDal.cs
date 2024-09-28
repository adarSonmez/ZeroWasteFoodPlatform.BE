using Core.DataAccess.Abstract;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Abstract.Marketing;

/// <summary>
/// Represents the data access layer interface for the Product entity.
/// </summary>
public interface IProductDal : IEntityRepository<Product>
{
}
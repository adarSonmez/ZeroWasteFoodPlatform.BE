using Core.DataAccess.Abstract;
using Domain.Entities.Association;

namespace DataAccess.Repositories.Abstract.Association;

/// <summary>
/// Represents the data access layer interface for the CategoryProduct entity.
/// </summary>
public interface ICategoryProductDal : IEntityRepository<CategoryProduct>
{
}
using Core.DataAccess.Abstract;
using Domain.Entities.Marketing;

namespace DataAccess.Repositories.Abstract.Marketing;

/// <summary>
/// Represents the data access layer interface for the Category entity.
/// </summary>
public interface ICategoryDal : IEntityRepository<Category>
{
}
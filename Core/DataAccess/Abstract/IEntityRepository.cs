using Core.Domain.Abstract;
using System.Linq.Expressions;

namespace Core.DataAccess.Abstract;

/// <summary>
/// Represents the interface for a generic entity repository.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface IEntityRepository<T> where T : class, IEntity, new()
{
    /// <summary>
    /// Retrieves all entities asynchronously.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="include">The function to include related entities.</param>
    /// <param name="orderBy">The function to order entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false, bool getDeleted = false);

    /// <summary>
    /// Retrieves all entities asynchronously with pagination.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="include">The function to include related entities.</param>
    /// <param name="orderBy">The function to order entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    Task<IEnumerable<T>> GetAllPaginatedAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false, bool getDeleted = false, int page = 1, int pageSize = int.MaxValue);

    /// <summary>
    /// Retrieves a single entity asynchronously based on the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the entity.</param>
    /// <param name="include">The function to include related entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <param name="getDeleted">Indicates whether to include deleted entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool enableTracking = false, bool getDeleted = false);

    /// <summary>
    /// Finds entities asynchronously based on the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="enableTracking">Indicates whether to enable entity tracking.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the queryable collection of entities.</returns>
    Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool enableTracking = false);

    /// <summary>
    /// Counts the number of entities asynchronously based on the predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities.</returns>
    Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null);

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Adds a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of added entities.</returns>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Performs a hard delete of an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteAsync(T entity);

    /// <summary>
    /// Performs a hard delete of an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteAsync(string id);

    /// <summary>
    /// Performs a hard delete of entities matching the provided IDs asynchronously.
    /// </summary>
    /// <param name="ids">The IDs of the entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteMatchingAsync(IEnumerable<string> ids);

    /// <summary>
    /// Performs a hard delete of entities matching the provided entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteMatchingAsync(params T[] entities);

    /// <summary>
    /// Performs a hard delete of entities matching the provided entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HardDeleteMatchingAsync(IEnumerable<T> entities);

    /// <summary>
    /// Performs a soft delete of an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteAsync(T entity);

    /// <summary>
    /// Performs a soft delete of an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteAsync(string id);

    /// <summary>
    /// Performs a soft delete of entities matching the provided IDs asynchronously.
    /// </summary>
    /// <param name="ids">The IDs of the entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteMatchingAsync(IEnumerable<string> ids);

    /// <summary>
    /// Performs a soft delete of entities matching the provided entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteMatchingAsync(params T[] entities);

    /// <summary>
    /// Performs a soft delete of entities matching the provided entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SoftDeleteMatchingAsync(IEnumerable<T> entities);
}
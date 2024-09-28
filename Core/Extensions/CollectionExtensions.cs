using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Core.Extensions;

/// <summary>
/// Provides extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Paginates a list of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    /// <param name="list">The list to paginate.</param>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>The paginated list of items.</returns>
    public static IList<T> Paginate<T>(this IEnumerable<T> list, int page, int pageSize)
    {
        if (page <= 0)
            page = 1;
        if (pageSize <= 0)
            pageSize = 10;

        return pageSize == int.MaxValue
            ? list.ToList()
            : list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// Paginates a queryable list of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    /// <param name="list">The queryable list to paginate.</param>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>The paginated list of items.</returns>
    public static IList<T> Paginate<T>(this IQueryable<T> list, int page, int pageSize)
    {
        if (page <= 0)
            page = 1;
        if (pageSize <= 0)
            pageSize = 10;

        return pageSize == int.MaxValue
            ? list.ToList()
            : list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// Asynchronously paginates a queryable list of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    /// <param name="list">The queryable list to paginate.</param>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>The paginated list of items.</returns>
    public static async Task<IList<T>> PaginateAsync<T>(this IQueryable<T> list, int page, int pageSize)
    {
        if (page <= 0)
            page = 1;
        if (pageSize <= 0)
            pageSize = 10;

        if (pageSize == int.MaxValue)
            return await list.ToListAsync();

        return await list
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Converts a dictionary to an object.
    /// </summary>
    /// <param name="dict">The dictionary to convert.</param>
    /// <returns>The converted object.</returns>
    public static object ToObject(this Dictionary<string, object> dict)
    {
        if (dict.Count == 0)
            return new object();

        var obj = new ExpandoObject();

        foreach (var pair in dict)
            obj.TryAdd(pair.Key, pair.Value);

        return obj;
    }
}
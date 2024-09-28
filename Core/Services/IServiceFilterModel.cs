using Core.Domain.Abstract;

namespace Core.Services;

/// <summary>
/// Represents a service filter model.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface IServiceFilterModel<T> where T : class, IEntity, new()
{
}
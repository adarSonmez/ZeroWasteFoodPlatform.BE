using Core.Domain.Abstract;

namespace Core.Services;

public interface IServiceFilterModel<T> where T : class, IEntity, new()
{
}
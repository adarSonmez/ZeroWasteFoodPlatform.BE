using Microsoft.Extensions.DependencyInjection;

namespace Core.Utils.DI.Abstract;

/// <summary>
/// Represents an interface for a dependency injection module.
/// </summary>
public interface IDependencyInjectionModule
{
    /// <summary>
    /// Loads the services into the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to load the services into.</param>
    void Load(IServiceCollection services);
}
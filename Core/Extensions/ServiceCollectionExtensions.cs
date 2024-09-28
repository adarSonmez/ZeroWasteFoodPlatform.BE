using Core.Utils.DI.Abstract;
using Core.Utils.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

/// <summary>
/// Extension methods for IServiceCollection to add dependency resolvers.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the dependency resolvers from the specified modules to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="modules">The modules containing the dependency resolvers.</param>
    /// <returns>The modified IServiceCollection instance.</returns>
    public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,
        IEnumerable<IDependencyInjectionModule> modules)
    {
        foreach (var module in modules)
            module.Load(services);

        return ServiceTool.Create(services);
    }
}
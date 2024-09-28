using Microsoft.Extensions.DependencyInjection;

namespace Core.Utils.IoC;

/// <summary>
/// A utility class for managing dependency injection using the Microsoft.Extensions.DependencyInjection framework.
/// </summary>
public static class ServiceTool
{
    private static IServiceProvider ServiceProvider { get; set; } = null!;

    /// <summary>
    /// Creates the service provider using the provided service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }

    /// <summary>
    /// Retrieves an instance of the specified service type from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of the service.</typeparam>
    /// <returns>An instance of the service, or null if the service is not registered.</returns>
    public static T? GetService<T>()
    {
        return ServiceProvider.GetService<T>();
    }
}
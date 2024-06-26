using Core.Utils.DI.Abstract;
using Core.Utils.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,
        IEnumerable<IDependencyInjectionModule> modules)
    {
        foreach (var module in modules)
            module.Load(services);

        return ServiceTool.Create(services);
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utils.IoC;

public interface IDependencyInjectionModule
{
    void Load(IServiceCollection services);
}
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utils.DI.Abstact;

public interface IDependencyInjectionModule
{
    void Load(IServiceCollection services);
}
using Microsoft.Extensions.DependencyInjection;

namespace Core.Models.DI;

public interface IDependencyInjectionModule
{
    void Load(IServiceCollection services);
}
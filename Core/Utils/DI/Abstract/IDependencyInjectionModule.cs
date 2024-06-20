using Microsoft.Extensions.DependencyInjection;

namespace Core.Utils.DI.Abstract;

public interface IDependencyInjectionModule
{
    void Load(IServiceCollection services);
}
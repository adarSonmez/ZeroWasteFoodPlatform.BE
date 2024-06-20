using Core.Utils.DI.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DependencyResolvers;

public sealed class DomainModule : IDependencyInjectionModule
{
    public void Load(IServiceCollection services)
    {
        #region Auto Mapper

        services.AddAutoMapper(typeof(DomainModule));

        #endregion Auto Mapper
    }
}
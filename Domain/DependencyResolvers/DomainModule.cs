using Core.Utils.DI.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DependencyResolvers;

/// <summary>
/// Represents the module responsible for configuring dependencies in the Domain layer.
/// </summary>
public sealed class DomainModule : IDependencyInjectionModule
{
    /// <inheritdoc/>
    public void Load(IServiceCollection services)
    {
        #region Auto Mapper

        services.AddAutoMapper(typeof(DomainModule));

        #endregion Auto Mapper
    }
}
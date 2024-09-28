using System.Diagnostics;
using Core.Security.SessionManagement;
using Core.Security.SessionManagement.Jwt;
using Core.Utils.DI.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers;

/// <summary>
/// Deoendency injection module for the Core layer.
/// </summary>
public class CoreModule : IDependencyInjectionModule
{
    /// <inheritdoc />
    public void Load(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<Stopwatch>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<ITokenHandler, JwtTokenHandler>();
    }
}
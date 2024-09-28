using Core.Utils.IoC;
using Core.Utils.Seed.Abstract;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions;

/// <summary>
/// Extension methods for the <see cref="IApplicationBuilder"/> interface.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the application to use a seeder for database seeding.
    /// </summary>
    /// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
    public static void UseSeeder(this IApplicationBuilder builder)
    {
        var seeder = ServiceTool.GetService<ISeeder>()!;
        seeder.Seed(builder);
    }
}
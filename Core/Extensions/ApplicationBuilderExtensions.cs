using Core.Utils.IoC;
using Core.Utils.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseSeeder(this IApplicationBuilder builder)
    {
        var seeder = ServiceTool.ServiceProvider.GetService<ISeeder>()!;
        seeder.Seed(builder);
    }
}
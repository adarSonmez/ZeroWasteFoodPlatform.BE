using Core.Utils.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Context;

public class DbContextBase : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>()!;
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
}
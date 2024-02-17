using Microsoft.EntityFrameworkCore;

namespace Core.Context;

public class DbContextBase : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=ZeroWasteFoodPlatform;Integrated Security=True;");
    }
}
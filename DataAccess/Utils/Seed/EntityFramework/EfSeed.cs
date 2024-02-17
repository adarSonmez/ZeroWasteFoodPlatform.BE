using Core.Constants;
using Core.Utils.Hashing;
using Core.Utils.Seed;
using DataAccess.Context.EntityFramework;
using Domain.Entities.Membership;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Utils.Seed.EntityFramework;

public class EfSeed : ISeeder
{
    public void Seed(IApplicationBuilder builder)
    {
        var context = builder.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<EfDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        // If there is no data in the database, then seed the database
        if (context.Category.Any())
            return;

        #region Customer

        HashingHelper.CreatePasswordHash("Su123456789.", out var passwordHash, out var passwordSalt);
        var adar = new Customer // Super User
        {
            Id = Guid.Empty,
            FirstName = "Adar",
            LastName = "Sönmez",
            Email = "adarsonmez@outlook.com",
            PhoneNumber = "+905452977501",
            Role = UserRoles.Admin,
            UseMultiFactorAuthentication = true,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        HashingHelper.CreatePasswordHash("Eren123456789.", out passwordHash, out passwordSalt);
        var eren = new Customer
        {
            FirstName = "Eren",
            LastName = "Karakaya",
            PhoneNumber = "+905365070289",
            Email = "erenkarakaya93@gmail.com",
            Role = UserRoles.Customer,
            UseMultiFactorAuthentication = true,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        HashingHelper.CreatePasswordHash("Baris123456789.", out passwordHash, out passwordSalt);
        var baris = new Customer
        {
            FirstName = "Barış",
            LastName = "Acar",
            PhoneNumber = "+90 5061809945",
            Email = "baris.acr99@gmail.com",
            Role = UserRoles.Customer,
            UseMultiFactorAuthentication = false,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var customers = new List<Customer> { adar, eren, baris };

        #endregion Customer
    }
}
using Core.Context.EntityFramework;
using Domain.Entities.Analytics;
using Domain.Entities.Association;
using Domain.Entities.Marketing;
using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context.EntityFramework;

public sealed class EfDbContext : EfDbContextBase
{
    public EfDbContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Category> Category { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<CategoryProduct> CategoryProduct { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<Report> Report { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .ToTable("Users", "Membership")
            .HasDiscriminator<string>("UserType")
            .HasValue<Business>("Business")
            .HasValue<Customer>("Customer");

        modelBuilder.Entity<Product>()
            .HasDiscriminator<string>("ProductType")
            .HasValue<MonitoredProduct>("MonitoredProduct")
            .HasValue<StoreProduct>("StoreProduct");

        modelBuilder.Entity<CategoryProduct>()
            .HasKey(cp => new { cp.CategoryId, cp.ProductId });

        modelBuilder.Entity<MonitoredProduct>()
            .HasOne(mp => mp.Owner)
            .WithMany(u => u.MonitoredProduct)
            .HasForeignKey(mp => mp.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StoreProduct>()
            .HasOne(sp => sp.Business)
            .WithMany(b => b.StoreProducts)
            .HasForeignKey(sp => sp.BusinessId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
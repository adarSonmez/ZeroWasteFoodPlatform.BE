using Core.Context;
using Domain.Entities.Analytics;
using Domain.Entities.Association;
using Domain.Entities.Marketing;
using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context.EntityFramework;

public sealed class EfDbContext : DbContextBase
{
    public EfDbContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Category> Category { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<CategoryProduct> CategoryProduct { get; set; } = null!;
    public DbSet<MonitoredProduct> MonitoredProduct { get; set; } = null!;
    public DbSet<StoreProduct> StoreProduct { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<Business> Business { get; set; } = null!;
    public DbSet<Customer> Customer { get; set; } = null!;
    public DbSet<Report> Report { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CategoryProduct>()
            .HasKey(cp => new { cp.CategoryId, cp.ProductId });

        modelBuilder.Entity<Customer>()
            .HasKey(c => c.UserId);

        modelBuilder.Entity<Business>()
            .HasKey(b => b.UserId);

        modelBuilder.Entity<StoreProduct>()
            .HasKey(sp => sp.ProductId);

        modelBuilder.Entity<MonitoredProduct>()
            .HasKey(mp => mp.ProductId);
    }
}
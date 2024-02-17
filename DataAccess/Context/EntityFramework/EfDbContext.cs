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

    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<CategoryProduct> CategoryProduct { get; set; } = default!;
    public DbSet<MonitoredProduct> MonitoredProduct { get; set; } = default!;
    public DbSet<StoreProduct> StoreProduct { get; set; } = default!;
    public DbSet<Business> Business { get; set; } = default!;
    public DbSet<Customer> Customer { get; set; } = default!;
    public DbSet<Report> Report { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryProduct>()
            .HasKey(p => new { p.CategoryId, p.ProductId });
    }
}
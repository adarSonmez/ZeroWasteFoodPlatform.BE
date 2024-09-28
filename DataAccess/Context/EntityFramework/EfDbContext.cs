using Core.Constants;
using Core.Context.EntityFramework;
using Core.Domain.Abstract;
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
        // ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Category> Category { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<CategoryProduct> CategoryProduct { get; set; } = null!;
    public DbSet<CustomerStoreProduct> CustomerStoreProduct { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<Report> Report { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        # region Shadow Properties

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                continue;

            modelBuilder.Entity(entityType.ClrType).Property<Guid?>(CommonShadowProperties.UpdatedUserId);
            modelBuilder.Entity(entityType.ClrType).Property<DateTime?>(CommonShadowProperties.UpdatedAt);
            modelBuilder.Entity(entityType.ClrType).Property<Guid?>(CommonShadowProperties.DeletedUserId);
            modelBuilder.Entity(entityType.ClrType).Property<DateTime?>(CommonShadowProperties.DeletedAt);
        }

        # endregion Shadow Properties

        # region Table Names and Schemas

        modelBuilder.HasDefaultSchema("Default");

        modelBuilder.Entity<Category>()
            .ToTable("Categories", "Marketing");

        modelBuilder.Entity<Product>()
            .ToTable("Products", "Marketing");

        modelBuilder.Entity<CategoryProduct>()
            .ToTable("CategoryProducts", "Association");

        modelBuilder.Entity<CustomerStoreProduct>()
            .ToTable("CustomerStoreProducts", "Association");

        modelBuilder.Entity<User>()
            .ToTable("Users", "Membership");

        modelBuilder.Entity<Report>()
            .ToTable("Reports", "Analytics");

        # endregion Table Names and Schemas

        # region Row Versions

        modelBuilder.Entity<Report>()
            .Property(p => p.RowVersion)
            .IsRowVersion();

        #endregion Row Versions

        #region Derived Entities (TPH)

        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Business>("Business")
            .HasValue<Customer>("Customer");

        modelBuilder.Entity<Product>()
            .HasDiscriminator<string>("ProductType")
            .HasValue<MonitoredProduct>("MonitoredProduct")
            .HasValue<StoreProduct>("StoreProduct");

        /* 
         * Here is an example of a TPT example:
         *    modelBuilder.Entity<Product>().ToTable("Products");
         *    modelBuilder.Entity<MonitoredProduct>().ToTable("MonitoredProducts");
         *    modelBuilder.Entity<StoreProduct>().ToTable("StoreProducts");
         * 
         * Here is an example of a TPC example:
         *    modelBuilder.Entity<Product>().UseTpcMappingStrategy();
         */

        #endregion Derived Entities

        #region Composite Keys

        modelBuilder.Entity<CategoryProduct>()
            .HasKey(cp => new { cp.CategoryId, cp.ProductId });

        modelBuilder.Entity<CustomerStoreProduct>()
            .HasKey(csp => new { csp.CustomerId, csp.ProductId });

        #endregion Composite Keys

        #region Auto Includes

        modelBuilder.Entity<Product>()
            .Navigation(p => p.Categories)
            .AutoInclude();

        #endregion Auto Includes

        #region Restricting Relationships

        modelBuilder.Entity<CustomerStoreProduct>()
            .HasOne(csp => csp.Customer)
            .WithMany(c => c.ShoppingList)
            .HasForeignKey(csp => csp.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CustomerStoreProduct>()
            .HasOne(csp => csp.Product)
            .WithMany(p => p.InterestedCustomers)
            .HasForeignKey(csp => csp.ProductId)
            .OnDelete(DeleteBehavior.Restrict);


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

        # endregion Restricting Relationships
    }
}
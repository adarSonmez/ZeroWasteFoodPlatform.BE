using Core.Constants.StringConstants;
using Core.Domain.Abstract;
using Core.Utils.Auth;
using Core.Utils.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Context.EntityFramework;

/// <summary>
/// Represents the base class for Entity Framework DbContext in the Core context.
/// </summary>
public class EfDbContextBase : DbContext
{
    /// <inheritdoc/>
    public override int SaveChanges()
    {
        OnBeforeSaveChanges();
        var result = base.SaveChanges();
        OnAfterSaveChanges();

        return result;
    }

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(token);
        OnAfterSaveChanges();

        return result;
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = ServiceTool.GetService<IConfiguration>()!;
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        // optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    /// <summary>
    /// Performs actions before saving changes in the DbContext.
    /// </summary>
    protected virtual void OnBeforeSaveChanges()
    {
    }

    /// <summary>
    /// Performs actions after saving changes in the DbContext.
    /// </summary>
    protected virtual void OnAfterSaveChanges()
    {
        var currentUserId = AuthHelper.GetUserId()!;

        var entries = ChangeTracker.Entries()
            .Where(e => e is { Entity: EntityBase, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            entityEntry.Property(CommonShadowProperties.UpdatedAt).CurrentValue = DateTime.UtcNow;
            entityEntry.Property(CommonShadowProperties.UpdatedUserId).CurrentValue = currentUserId;

            if (entityEntry.State == EntityState.Added)
            {
                ((EntityBase)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                ((EntityBase)entityEntry.Entity).CreatedUserId = currentUserId ?? Guid.Empty;
            }

            // Soft delete handling
            if (entityEntry.Property("IsDeleted")?.CurrentValue is not bool isDeleted || !isDeleted)
                continue;

            entityEntry.Property(CommonShadowProperties.DeletedAt).CurrentValue = DateTime.UtcNow;
            entityEntry.Property(CommonShadowProperties.DeletedUserId).CurrentValue = currentUserId;
        }
    }
}
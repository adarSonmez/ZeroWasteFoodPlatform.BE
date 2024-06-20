using Core.Constants;
using Core.Domain.Abstract;
using Core.Utils.Auth;
using Core.Utils.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Context.EntityFramework;

public class EfDbContextBase : DbContext
{
    private readonly IHttpContextAccessor? _httpContextAccessor = ServiceTool.GetService<IHttpContextAccessor>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = ServiceTool.GetService<IConfiguration>()!;
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        // optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public override int SaveChanges()
    {
        OnBeforeSaveChanges();
        var result = base.SaveChanges();
        OnAfterSaveChanges();

        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(token);
        OnAfterSaveChanges();

        return result;
    }

    protected virtual void OnBeforeSaveChanges()
    {
        // do something before save changes
    }

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

            if (entityEntry.State != EntityState.Deleted)
                continue;

            // TODO:Handle soft delete
            entityEntry.Property(CommonShadowProperties.DeletedAt).CurrentValue = DateTime.UtcNow;
            entityEntry.Property(CommonShadowProperties.DeletedUserId).CurrentValue = currentUserId;
        }
    }
}
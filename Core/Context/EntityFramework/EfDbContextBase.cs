using System.Security.Claims;
using Core.Domain.Abstract;
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
        var currentUserIdStr = _httpContextAccessor?.HttpContext?.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        var currentUserId = currentUserIdStr is not null ? Guid.Parse(currentUserIdStr) : Guid.Empty;

        var entries = ChangeTracker.Entries()
            .Where(e => e is { Entity: EntityBase, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            ((EntityBase)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            ((EntityBase)entityEntry.Entity).UpdatedUserId = currentUserId;

            if (entityEntry.State != EntityState.Deleted)
                continue;

            ((EntityBase)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
            ((EntityBase)entityEntry.Entity).DeletedUserId = currentUserId;
        }
    }
}
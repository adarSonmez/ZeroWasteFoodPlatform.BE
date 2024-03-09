using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Abstract;

public abstract class EntityBase : IEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? DeletedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid CreatedUserId { get; set; } = Guid.Empty;

    public Guid? DeletedUserId { get; set; }

    public Guid? UpdatedUserId { get; set; }
}
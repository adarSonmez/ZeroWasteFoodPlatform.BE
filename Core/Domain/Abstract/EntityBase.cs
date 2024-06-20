using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Abstract;

public abstract class EntityBase : IEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsDeleted { get; set; }

    public Guid CreatedUserId { get; set; } = Guid.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
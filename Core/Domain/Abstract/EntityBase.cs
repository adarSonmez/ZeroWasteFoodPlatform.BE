using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Abstract;

/// <summary>
/// Represents the base class for all entities in the domain.
/// </summary>
public abstract class EntityBase : IEntity
{
    /// <summary>
    /// Gets or sets the ID of the entity.
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the entity.
    /// </summary>
    public Guid CreatedUserId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
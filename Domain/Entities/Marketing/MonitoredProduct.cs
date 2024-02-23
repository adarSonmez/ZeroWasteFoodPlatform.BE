using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Abstract;

namespace Domain.Entities.Marketing;

[Table("MonitoredProducts", Schema = "Marketing")]
public class MonitoredProduct : IEntity
{
    [Key] public Guid ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
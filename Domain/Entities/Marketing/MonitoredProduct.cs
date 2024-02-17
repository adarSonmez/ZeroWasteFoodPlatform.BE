using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Marketing;

[Table("MonitoredProducts", Schema = "Marketing")]
public class MonitoredProduct : Product
{
}
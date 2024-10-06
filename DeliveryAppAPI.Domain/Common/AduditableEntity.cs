using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Domain.Common;

public class AuditableEntity
{
    [MaxLength(150)]
    public string CreatedBy { get; set; } = default!;

    public DateTime Created { get; set; }

    [MaxLength(150)]
    public string ModifiedBy { get; set; } = default!;

    public DateTime? Modified { get; set; }
}

using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class TransportationItem : AuditableEntity
{
    [Column("TransportationItemId")]
    public int Id { get; set; }
    public int TransportationId { get; set; }
    public Transportation Transportation { get; set; }
    public int PackageId { get; set; }
    public Package Package { get; set; }
}

using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Transportation : AuditableEntity
{
    [Column("TransportationId")]
    public int Id { get; set; }
    public int AssignedDriverId { get; set; }
    public Driver AssignedDriver { get; set; }
    public int TransportationStatusId { get; set; }
    public Dictionary TransportationStatus { get; set; }
    public DateTime DateOfTransport {  get; set; }

    public ICollection<TransportationItem> TransportationItems { get; set; }
}

using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Driver : AuditableEntity
{
    [Column("DriverId")]
    public int Id { get; set; }
    public string BaseUserId { get; set; }
    public User BaseUser { get; set; }
    public int AssignedCarId {  get; set; }
    public Car AssignedCar { get; set; }
}

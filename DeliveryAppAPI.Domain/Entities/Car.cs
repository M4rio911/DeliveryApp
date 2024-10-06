using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Car : AuditableEntity
{
    [Column("CarId")]
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Seats { get; set; }
    public int EngineCapacity { get; set; }
    public int HorsePower { get; set; }
    public decimal MaxLoad { get; set; }
}

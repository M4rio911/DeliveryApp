using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Car : AuditableEntity
{
    [Column("CarId")]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal MaxLoad { get; set; }
}

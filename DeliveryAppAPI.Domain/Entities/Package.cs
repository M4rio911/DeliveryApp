using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Package :AuditableEntity
{
    [Column("PackageId")]
    public int Id { get; set; }
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public int? ReciverId { get; set; }
    public User? Reciver { get; set; }
    public int? CountryId { get; set; }
    public Country? Country { get; set; }
    public int DestinationId { get; set; }
    public Address Destination { get; set; }
    public int PackageTypeId { get; set; }
    public Dictionary PackageType { get; set; }
    public int PackageStatusId { get; set; }
    public Dictionary PackageStatus { get; set; }
    public int PaymentId { get; set; }
    public Payment Payment { get; set; }
}

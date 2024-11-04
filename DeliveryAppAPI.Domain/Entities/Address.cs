using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Address : AuditableEntity
{
    [Column("AddressId")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int? CountryId { get; set; }
    public Country Country { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public int AddressTypeId { get; set; }
    public Dictionary AddressType {  get; set; }
}

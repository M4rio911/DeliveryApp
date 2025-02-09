using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class PackagePrice : AuditableEntity
{
    [Column("PackagePriceId")]
    public int Id { get; set; }
    public int PackageTypeId { get; set; }
    public Dictionary PackageType { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public decimal Price { get; set; }
}

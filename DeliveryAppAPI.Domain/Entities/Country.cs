using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Country : AuditableEntity
{
    [Column("CountryId")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int? CurrencyId { get; set; }
    public Currency Currency { get; set; }
}

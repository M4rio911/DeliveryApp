using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class DictionaryType : AuditableEntity
{
    [Column("DictionaryTypeId")]
    public int Id { get; set; }
    public string Name { get; set; }
}

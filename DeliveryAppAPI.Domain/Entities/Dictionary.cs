using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Dictionary : AuditableEntity
{
    [Column("DictionaryId")]
    public int Id { get; set; }
    public int DictionaryTypeId { get; set; }
    public DictionaryType DictionaryType { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
}

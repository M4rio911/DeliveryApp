using DeliveryApp.Domain.Common;

namespace DeliveryApp.Domain.Entities;

public class StoragePackages : AuditableEntity
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public Package Package { get; set; }
    public DateTime DateOfArrival { get; set; }
    public DateTime DateOfExit { get; set; }
}

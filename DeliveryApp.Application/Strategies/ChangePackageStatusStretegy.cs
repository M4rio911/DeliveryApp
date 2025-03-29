using DeliveryApp.Application.Interfaces.Strategies;
using DeliveryApp.Infrastructure.Models;

namespace DeliveryApp.Infrastructure.Strategies;

public class ChangePackageStatusStretegy : IChangePackageStatusStretegy
{
    public static PackageStatusEnum GetNextPackageStatus(PackageStatusEnum currentPackageStatus)
    {
        return currentPackageStatus switch
        {
            PackageStatusEnum.New
            => PackageStatusEnum.Paid,

            PackageStatusEnum.Paid
            => PackageStatusEnum.Posted,

            PackageStatusEnum.Posted
            => PackageStatusEnum.AssignedToCollect,

            PackageStatusEnum.AssignedToCollect
            => PackageStatusEnum.Collected,

            PackageStatusEnum.Collected
            => PackageStatusEnum.Storage,

            PackageStatusEnum.Storage
            => PackageStatusEnum.AssignedToDelivery,

            PackageStatusEnum.AssignedToDelivery
            => PackageStatusEnum.IssuedToDelivery,

            PackageStatusEnum.IssuedToDelivery
            => PackageStatusEnum.Delivered,

            PackageStatusEnum.Delivered
            => PackageStatusEnum.Completed
        };
    }

    public static PackageStatusEnum ConvertToPublicPackageStatus(PackageStatusEnum packageStatus)
    {
        return packageStatus switch
        {
            PackageStatusEnum.AssignedToCollect or
            PackageStatusEnum.Collected or
            PackageStatusEnum.Storage or
            PackageStatusEnum.AssignedToDelivery
            => PackageStatusEnum.InDelivery,

            _ => packageStatus
            
        };
    }
}

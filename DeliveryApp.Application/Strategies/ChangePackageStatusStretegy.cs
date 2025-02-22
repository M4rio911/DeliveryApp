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
            => PackageStatusEnum.Collected,

            PackageStatusEnum.Collected
            => PackageStatusEnum.Storage,

            PackageStatusEnum.Storage
            => PackageStatusEnum.IssuedToDelivery,

            PackageStatusEnum.IssuedToDelivery
            => PackageStatusEnum.InDelivery,

            PackageStatusEnum.InDelivery
            => PackageStatusEnum.Delivered,

            PackageStatusEnum.Delivered
            => PackageStatusEnum.Completed
        };
    }
}

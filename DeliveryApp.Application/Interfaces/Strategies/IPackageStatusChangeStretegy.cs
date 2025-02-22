using DeliveryApp.Infrastructure.Models;

namespace DeliveryApp.Application.Interfaces.Strategies
{
    public interface IChangePackageStatusStretegy
    {
        static abstract PackageStatusEnum GetNextPackageStatus(PackageStatusEnum currentPackageStatus);
    }
}

namespace DeliveryApp.Persistance.Models;

public enum PackageStatusEnum
{
    Ordered,
    Paid,

    ToCollect,
    CollectedFromSender,
    CentralWarehouse,
    WayToFinalDestination,
    Delivered,

    Delayed,
    ReturnedToSender,
    Cancelled,
    Lost
}

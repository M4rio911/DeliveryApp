namespace DeliveryApp.Infrastructure.Models;

public enum PackageStatusEnum
{
    New,
    Paid,
    Posted,
    AssignedToCollect,
    Collected,
    Storage,
    AssignedToDelivery,
    InDelivery,
    IssuedToDelivery,
    Delivered,
    Completed

    //ReturnedToSender,
    //Cancelled,
    //Lost
}

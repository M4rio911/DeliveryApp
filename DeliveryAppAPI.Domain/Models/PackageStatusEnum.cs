﻿namespace DeliveryApp.Infrastructure.Models;

public enum PackageStatusEnum
{
    New,
    Paid,
    Posted,
    Collected,
    Storage,
    IssuedToDelivery,
    InDelivery,
    Delivered,
    Completed

    //ReturnedToSender,
    //Cancelled,
    //Lost
}

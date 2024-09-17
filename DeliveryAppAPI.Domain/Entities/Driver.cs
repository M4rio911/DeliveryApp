﻿using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Common;

namespace DeliveryApp.Domain.Entities;

public class Driver : AuditableEntity
{
    public int Id { get; set; }
    public int BaseUserId { get; set; }
    public User BaseUser { get; set; }
    public int AssignedCarId {  get; set; }
    public Car AssignedCar { get; set; }
}

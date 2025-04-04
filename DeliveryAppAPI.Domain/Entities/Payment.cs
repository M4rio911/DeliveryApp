﻿using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Payment :AuditableEntity
{
    [Column("PaymentId")]
    public int Id { get; set; }
    public int PaymentTypeId { get; set; }
    public Dictionary PaymentType { get; set; }
    public int PaymentStatusId { get; set; }
    public Dictionary PaymentStatus { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public decimal Price { get; set; }
}

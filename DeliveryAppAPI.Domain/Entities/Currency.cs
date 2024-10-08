﻿using DeliveryApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class Currency : AuditableEntity
{
    [Column("CurrencyId")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Shortcut { get; set; }
}

﻿namespace DeliveryApp.Application.Models;

public class ResetPasswordModel
{
    public string Email { get; set; }
    public string UserId { get; set; }
    public string NewPassword { get; set; }
}

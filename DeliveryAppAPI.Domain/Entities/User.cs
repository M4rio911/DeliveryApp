using DeliveryApp.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool ActiveStatus { get; set; } 
    public int UserTypeId { get; set; }
    public Dictionary UserType { get; set; }
}

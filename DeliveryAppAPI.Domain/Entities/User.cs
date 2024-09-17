using DeliveryApp.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.Domain.Entities;

public class User : IdentityUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool ActiveStatus { get; set; } 
    public int UserTypeId { get; set; }
    public Dictionary UserType { get; set; }
}

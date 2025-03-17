using DeliveryApp.Application.Dto.Cars;

namespace DeliveryApp.Application.Dto.Users;

public class GetDriverDto
{
    public int Id { get; set; }
    public string BaseUserId { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? AssignedCarId { get; set; }
    public GetCarsDto? Car { get; set; }
}

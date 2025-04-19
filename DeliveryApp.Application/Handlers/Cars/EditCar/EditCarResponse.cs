using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Cars.EditCar;

public class EditCarResponse : BaseResponse
{
    public GetCarsDto Car { get; set; }
    public EditCarResponse(Car car) : base(true, null, true) 
    {
        Car = new GetCarsDto
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            Seats = car.Seats,
            EngineCapacity = car.EngineCapacity,
            HorsePower = car.HorsePower,
            MaxLoad = car.MaxLoad
        };
    }
    public EditCarResponse(string error) : base(error) { }
}

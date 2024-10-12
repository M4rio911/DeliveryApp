using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Cars.EditCar;

public class EditCar : ICommand<EditCarResponse>
{
    public int Id { get; set; }
    public string Brand {  get; set; }
    public string Model {  get; set; }
    public int Year {  get; set; }
    public int Seats {  get; set; }
    public int EngineCapacity {  get; set; }
    public int HorsePower {  get; set; }
    public int MaxLoad {  get; set; }

    public EditCar(EditCarParameters parameters)
    {
        Id = parameters.Id;
        Brand = parameters.Brand;
        Model = parameters.Model;
        Year = parameters.Year;
        Seats = parameters.Seats;
        EngineCapacity = parameters.EngineCapacity;
        HorsePower = parameters.HorsePower;
        MaxLoad = parameters.MaxLoad;
    }
}
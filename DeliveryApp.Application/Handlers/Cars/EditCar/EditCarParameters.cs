namespace DeliveryApp.Application.Handlers.Cars.EditCar;

public class EditCarParameters
{
    public int Id { get; set; }
    public string Brand {  get; set; }
    public string Model {  get; set; }
    public int Year {  get; set; }
    public int Seats {  get; set; }
    public int EngineCapacity {  get; set; }
    public int HorsePower {  get; set; }
    public int MaxLoad {  get; set; }
}
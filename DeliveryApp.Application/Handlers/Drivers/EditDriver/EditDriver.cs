using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Drivers.EditDriver;

public class EditDriver : ICommand<EditDriverResponse>
{
    public int DriverId { get; set; }
    public string UserId { get; set; }
    public int AssignedCarId { get; set; }

    public EditDriver(EditDriverParameters parameters)
    {
        DriverId = parameters.DriverId;
        UserId = parameters.UserId;
        AssignedCarId = parameters.AssignedCarId;
    }
}
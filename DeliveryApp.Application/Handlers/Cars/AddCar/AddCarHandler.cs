using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Cars.AddCar;

public class AddCarHandler : ICommandHandler<AddCar, AddCarResponse>
{
    //private readonly 
    private readonly DeliveryDbContext _context;

    public AddCarHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<AddCarResponse> Handle(AddCar request, CancellationToken cancellationToken)
    {
        var newCar = new Car
        {
            Brand = request.Brand,
            Model = request.Model,
            HorsePower = request.HorsePower,
            EngineCapacity = request.EngineCapacity,
            Year = request.Year,
            Seats = request.Seats,
            MaxLoad = request.MaxLoad,
            Created = DateTime.UtcNow,
            CreatedBy = "testUser",
            ModifiedBy = "testUser"
        };


        _context.Cars.Add(newCar);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddCarResponse();
    }
}

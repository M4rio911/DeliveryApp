using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;

namespace DeliveryApp.Application.Handlers.Cars.RemoveCar;

public class RemoveCarHandler : ICommandHandler<RemoveCar, RemoveCarResponse>
{
    //private readonly 
    private readonly DeliveryDbContext _context;

    public RemoveCarHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<RemoveCarResponse> Handle(RemoveCar request, CancellationToken cancellationToken)
    {
        var carToRemove = _context.Cars
            .FirstOrDefault(x => x.Id == request.CarId);

        _context.Cars.Remove(carToRemove);
        await _context.SaveChangesAsync();

        return new RemoveCarResponse();
    }
}

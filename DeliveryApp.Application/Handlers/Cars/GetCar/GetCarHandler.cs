using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Cars.GetCar;

public class GetCarHandler : IQueryHandler<GetCar, GetCarResponse>
{
    private readonly DeliveryDbContext _context;

    public GetCarHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetCarResponse> Handle(GetCar request, CancellationToken cancellationToken)
    {
        var response = await _context.Cars
            .Where(x => x.Id == request.CarId)
            .Select(x => new GetCarsDto() 
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model,
                Year = x.Year,
                Seats = x.Seats,
                EngineCapacity = x.EngineCapacity,
                HorsePower = x.HorsePower,
                MaxLoad = x.MaxLoad
            }).FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return new GetCarResponse("No car was found under passed Id");

        return new GetCarResponse()
        {
            Car = response
        };
    }
}

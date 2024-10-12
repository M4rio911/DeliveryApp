using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Cars.GetCars;

public class GetCarsHandler : IQueryHandler<GetCars, GetCarsResponse>
{
    private readonly DeliveryDbContext _context;

    public GetCarsHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetCarsResponse> Handle(GetCars request, CancellationToken cancellationToken)
    {
        var response = await _context.Cars
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
            }).ToListAsync();

        return new GetCarsResponse()
        {
            Cars = response
        };
    }
}

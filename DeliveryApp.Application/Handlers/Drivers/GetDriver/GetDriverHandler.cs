using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Drivers.GetDriverByUserId;

public class GetDriverHandler : IQueryHandler<GetDriver, GetDriverResponse>
{
    private readonly DeliveryDbContext _context;

    public GetDriverHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetDriverResponse> Handle(GetDriver request, CancellationToken cancellationToken)
    {
        var response = await _context.Drivers
            .Include(x => x.AssignedCar)
            .Where(x => x.BaseUserId == request.UserId)
            .Select(x => new GetDriverDto()
            {
                Id = x.Id,
                BaseUserId = x.BaseUserId,
                AssignedCarId = x.AssignedCarId,
                Car = x.AssignedCar == null ? null : 
                new GetCarsDto()
                {
                    Brand = x.AssignedCar.Brand,
                    Model = x.AssignedCar.Model,
                    EngineCapacity = x.AssignedCar.EngineCapacity,
                    HorsePower = x.AssignedCar.HorsePower,
                    Year = x.AssignedCar.Year,
                    Id = x.AssignedCar.Id
                }
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new GetDriverResponse()
        {
            Driver = response
        };
    }
}

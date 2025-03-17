using DeliveryApp.Application.Dto.Cars;
using DeliveryApp.Application.Dto.Currencies;
using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Drivers.GetDrivers;

public class GetDriversHandler : IQueryHandler<GetDrivers, GetDriversResponse>
{
    private readonly DeliveryDbContext _context;

    public GetDriversHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<GetDriversResponse> Handle(GetDrivers request, CancellationToken cancellationToken)
    {
        var response = await _context.Drivers
            .Include(x => x.AssignedCar)
            .Include(x => x.BaseUser)
            .Where(x => x.AssignedCarId != null)
            .Select(x => new GetDriverDto() 
            {
                Id = x.Id,
                BaseUserId = x.BaseUserId,
                FirstName = x.BaseUser.FirstName,
                LastName = x.BaseUser.LastName,
                Login = x.BaseUser.UserName,
                AssignedCarId = x.AssignedCarId,
                Car = new GetCarsDto()
                {
                    Brand = x.AssignedCar.Brand,
                    Model = x.AssignedCar.Model,
                    EngineCapacity = x.AssignedCar.EngineCapacity,
                    HorsePower = x.AssignedCar.HorsePower,
                    Year = x.AssignedCar.Year,
                    Id = x.AssignedCar.Id
                }
            })
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return new GetDriversResponse()
        {
            Drivers = response
        };
    }
}

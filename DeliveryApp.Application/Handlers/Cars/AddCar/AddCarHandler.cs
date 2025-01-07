using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Handlers.Cars.AddCar;

public class AddCarHandler : ICommandHandler<AddCar, AddCarResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public AddCarHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<AddCarResponse> Handle(AddCar request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

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
            CreatedBy = user,
            ModifiedBy = user
        };


        _context.Cars.Add(newCar);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddCarResponse();
    }
}

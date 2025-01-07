using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Cars.EditCar;

public class EditCarHandler : ICommandHandler<EditCar, EditCarResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public EditCarHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditCarResponse> Handle(EditCar request, CancellationToken cancellationToken)
    {
        var dbCar = await _context.Cars
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbCar == null) 
            return new EditCarResponse("No car was found under passed ID");

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        dbCar.Brand = request.Brand;
        dbCar.Model = request.Model;
        dbCar.HorsePower = request.HorsePower;
        dbCar.EngineCapacity = request.EngineCapacity;
        dbCar.Year = request.Year;
        dbCar.Seats = request.Seats;
        dbCar.MaxLoad = request.MaxLoad;
        dbCar.Modified = DateTime.UtcNow;
        dbCar.ModifiedBy = user;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditCarResponse(dbCar);
    }
}

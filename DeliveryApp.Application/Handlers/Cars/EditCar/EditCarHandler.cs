using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Cars.EditCar;

public class EditCarHandler : ICommandHandler<EditCar, EditCarResponse>
{
    private readonly DeliveryDbContext _context;

    public EditCarHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<EditCarResponse> Handle(EditCar request, CancellationToken cancellationToken)
    {
        var dbCar = await _context.Cars
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbCar == null) 
            return new EditCarResponse("No car was found under passed ID");

        dbCar.Brand = request.Brand;
        dbCar.Model = request.Model;
        dbCar.HorsePower = request.HorsePower;
        dbCar.EngineCapacity = request.EngineCapacity;
        dbCar.Year = request.Year;
        dbCar.Seats = request.Seats;
        dbCar.MaxLoad = request.MaxLoad;
        dbCar.Modified = DateTime.UtcNow;
        
        //User to do 
        dbCar.ModifiedBy = "ModifiedUser";

        await _context.SaveChangesAsync(cancellationToken);

        return new EditCarResponse(dbCar);
    }
}

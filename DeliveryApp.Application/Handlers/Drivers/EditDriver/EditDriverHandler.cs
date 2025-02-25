﻿using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.Drivers.EditDriver;

public class EditDriverHandler : ICommandHandler<EditDriver, EditDriverResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public EditDriverHandler(IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditDriverResponse> Handle(EditDriver request, CancellationToken cancellationToken)
    {
        var dbDriver = await _context.Drivers
            .Where(x => x.Id == request.DriverId
                && x.BaseUserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbDriver == null) 
            return new EditDriverResponse("No car was found under passed ID");

        var user = _httpContextAccessor.HttpContext?.User.Identities.FirstOrDefault().Name;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        if(dbDriver.AssignedCarId == request.AssignedCarId ||
           dbDriver.AssignedCarId == null && request.AssignedCarId == 0)
            return new EditDriverResponse(dbDriver);

        var dbOldCar = await _context.Cars
            .Where(x => x.Id == dbDriver.AssignedCarId)
            .FirstOrDefaultAsync(cancellationToken);

        if (request.AssignedCarId == 0)
        {
            // Przypadek 3: CAR -> NULL
            if (dbOldCar != null)
            {
                dbOldCar.AssignedUserId = null;
                dbOldCar.ModifiedBy = user;
                dbOldCar.Modified = DateTime.UtcNow;
            }

            dbDriver.AssignedCarId = null;
        }
        else
        {
            var dbNewCar = await _context.Cars
                .Where(x => x.Id == request.AssignedCarId)
                .FirstOrDefaultAsync(cancellationToken);

            if (dbNewCar == null)
                return new EditDriverResponse("New car was not found under passed ID");

            // Przypadek 1: NULL -> CAR
            // Przypadek 2: CAR -> CAR

            if (dbOldCar != null)
            {
                dbOldCar.AssignedUserId = null;
                dbOldCar.ModifiedBy = user;
                dbOldCar.Modified = DateTime.UtcNow;
            }

            dbNewCar.AssignedUserId = request.UserId;
            dbNewCar.ModifiedBy = user;
            dbNewCar.Modified = DateTime.UtcNow;

            dbDriver.AssignedCarId = request.AssignedCarId;
        }

        dbDriver.ModifiedBy = user;
        dbDriver.Modified = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditDriverResponse(dbDriver);
    }
}

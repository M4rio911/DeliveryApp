using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using DeliveryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Handlers.User.EditUser;

public class EditUserHandler : ICommandHandler<EditUser, EditUserResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DeliveryDbContext _context;

    public EditUserHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
    }

    public async Task<EditUserResponse> Handle(EditUser request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }
        var userName = user.Identities.FirstOrDefault().Name;

        var dbUser = await _context.Users
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbUser == null) 
            return new EditUserResponse("No user was found under passed ID");

        var driverUserTypeId = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.UserType.ToString(), UserTypeEnum.Delivery.ToString())).Id;
        if(driverUserTypeId == 0)
            return new EditUserResponse("Dictionary: UserType - errror");

        dbUser.UserName = request.UserName;
        dbUser.FirstName = request.FirstName;
        dbUser.LastName = request.LastName;
        dbUser.Email = request.Email;
        dbUser.PhoneNumber = request.PhoneNumber;

        //USER TYPE NOT CHANGED
        if (request.UserType == dbUser.UserTypeId)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return new EditUserResponse(dbUser);
        }

        //IF NOT DRIVER && WAS NOT DRIVER
        if (driverUserTypeId != request.UserType
        && driverUserTypeId != dbUser.UserTypeId)
        {
            dbUser.UserTypeId = request.UserType;

            await _context.SaveChangesAsync(cancellationToken);
            return new EditUserResponse(dbUser);
        }

        var existingDriver = await _context.Drivers
            .Where(x => x.BaseUserId == dbUser.Id)
            .FirstOrDefaultAsync(cancellationToken);

        //IF WAS DRIVER
        if(dbUser.UserTypeId == driverUserTypeId &&
            existingDriver.AssignedCarId != null)
            return new EditUserResponse("Error - Driver has assigned car");

        dbUser.UserTypeId = request.UserType;

        //IF TYPE DRIVER
        if (existingDriver == null) {
            var newDriver = new Driver
            {
                BaseUserId = dbUser.Id,
                Created = DateTime.UtcNow,
                CreatedBy = userName
            };
            _context.Drivers.Add(newDriver);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new EditUserResponse(dbUser);
    }
}

using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Infrastructure.Models;
using DeliveryApp.Persistance;
using DeliveryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DeliveryApp.Application.Handlers.User.EditUser;

public class EditUserHandler : ICommandHandler<EditUser, EditUserResponse>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly DeliveryDbContext _context;

    public EditUserHandler(IDictionaryRepository dictionaryRepository, IHttpContextAccessor httpContextAccessor, DeliveryDbContext deliveryDbContext, UserManager<Domain.Entities.User> userManager)
    {
        _dictionaryRepository = dictionaryRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = deliveryDbContext;
        _userManager = userManager;
    }

    public async Task<EditUserResponse> Handle(EditUser request, CancellationToken cancellationToken)
    {
        var loggedClaimsUser = _httpContextAccessor.HttpContext?.User;
        if (loggedClaimsUser == null)
        {
            return new EditUserResponse("User is not authenticated");
        }

        var userEmail = loggedClaimsUser.FindFirst(ClaimTypes.Email)?.Value;
        var userName = loggedClaimsUser.FindFirst(ClaimTypes.Name)?.Value;

        var identityUser = await _userManager.FindByIdAsync(request.Id);
        if (identityUser == null)
        {
            return new EditUserResponse("User not found in database");
        }

        //EMAIL CHANGED
        if (request.Email != identityUser.Email)
        {
            var userInDb = await _userManager.FindByEmailAsync(request.Email);
            if (userInDb != null)
            {
                return new EditUserResponse("Email already taken.");
            }
        }

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

        await _userManager.SetEmailAsync(dbUser, request.Email);

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
            
            //ROLE
            if (!await AssignNewRole(dbUser))
                return new EditUserResponse("Failed to remove current roles");

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
        if (!await AssignNewRole(dbUser))
            return new EditUserResponse("Failed to remove current roles");

        //CAR - IF TYPE DRIVER
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

    private async Task<bool> AssignNewRole(Domain.Entities.User dbUser)
    {
        var newRoleType = (await _dictionaryRepository.GetDictionary(DictionaryTypeEnum.UserType.ToString(), dbUser.UserTypeId)).Name;

        var currentRoles = await _userManager.GetRolesAsync(dbUser);

        var removeResult = await _userManager.RemoveFromRolesAsync(dbUser, currentRoles);
        if (!removeResult.Succeeded) return false;

        var addResult = await _userManager.AddToRoleAsync(dbUser, newRoleType);

        return addResult.Succeeded;  
    }
}

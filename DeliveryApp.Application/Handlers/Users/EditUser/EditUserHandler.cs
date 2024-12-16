using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.User.EditUser;

public class EditUserHandler : ICommandHandler<EditUser, EditUserResponse>
{
    private readonly DeliveryDbContext _context;

    public EditUserHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<EditUserResponse> Handle(EditUser request, CancellationToken cancellationToken)
    {
        var dbUser = await _context.Users
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbUser == null) 
            return new EditUserResponse("No user was found under passed ID");

        dbUser.UserName = request.UserName;
        dbUser.FirstName = request.FirstName;
        dbUser.LastName = request.LastName;
        dbUser.Email = request.Email;
        dbUser.PhoneNumber = request.PhoneNumber;
        dbUser.UserTypeId = request.UserType;

        await _context.SaveChangesAsync(cancellationToken);

        return new EditUserResponse(dbUser);
    }
}

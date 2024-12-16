using DeliveryApp.Application.Interfaces.Mediator;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Handlers.User.ChangeActiveStatus;

public class ChangeActiveStatusHandler : ICommandHandler<ChangeActiveStatus, ChangeActiveStatusResponse>
{
    private readonly DeliveryDbContext _context;

    public ChangeActiveStatusHandler(DeliveryDbContext deliveryDbContext)
    {
        _context = deliveryDbContext;
    }

    public async Task<ChangeActiveStatusResponse> Handle(ChangeActiveStatus request, CancellationToken cancellationToken)
    {
        var dbUser = await _context.Users
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if(dbUser == null) 
            return new ChangeActiveStatusResponse("No user was found under passed ID");

        dbUser.ActiveStatus = request.NewActiveStatus;

        await _context.SaveChangesAsync(cancellationToken);

        return new ChangeActiveStatusResponse();
    }
}

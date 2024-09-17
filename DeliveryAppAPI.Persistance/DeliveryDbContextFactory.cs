using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Persistance;

public class DeliveryDbContextFactory : DesignTimeDbContextFactoryBase<DeliveryDbContext>
{
    protected override DeliveryDbContext CreateNewInstance(DbContextOptions<DeliveryDbContext> options)
    {
        return new DeliveryDbContext(options);
    }
}

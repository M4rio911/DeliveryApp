using DeliveryApp.Domain.Common;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Persistance;

public class DeliveryDbContext : IdentityDbContext<User>
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

    public DbSet<Address> Address { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Dictionary> Dictionaries { get; set; }
    public DbSet<DictionaryType> DictionaryTypes { get; set; }
    public DbSet<Driver> Drivers{ get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<StoragePackages> StoragePackages { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Transportation> Transportations { get; set; }
    public DbSet<TransportationItem> TransportationItems { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Table Properties

        builder.Entity<Address>(b =>
        {
            b.HasKey(a => a.Id);
            b.Property(a => a.City).HasMaxLength(50);
            b.Property(a => a.Street).HasMaxLength(50);
            b.Property(a => a.Number).HasMaxLength(20);
        });
        builder.Entity<Car>(b =>
        {
            b.HasKey(c => c.Id);
        });
        builder.Entity<Country>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).HasMaxLength(50);
            b.Property(c => c.Code).HasMaxLength(10);
        });
        builder.Entity<Currency>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).HasMaxLength(50);
            b.Property(c => c.Shortcut).HasMaxLength(10);
        });
        builder.Entity<Dictionary>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).HasMaxLength(100);
        });
        builder.Entity<DictionaryType>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).HasMaxLength(50);
        });
        builder.Entity<Driver>(b =>
        {
            b.HasKey(c => c.Id);
        });
        builder.Entity<Package>(b =>
        {
            b.HasKey(c => c.Id);
        });
        builder.Entity<StoragePackages>(b =>
        {
            b.HasKey(c => c.Id);
        });
        builder.Entity<Payment>(b =>
        {
            b.HasKey(c => c.Id);
        });
        builder.Entity<Transportation>(b =>
        {
            b.HasKey(c => c.Id);
        });
        builder.Entity<TransportationItem>(b =>
        {
            b.HasKey(c => c.Id);
        });
        builder.Entity<User>(b =>
        {
            b.Property(u => u.FirstName).HasMaxLength(30);
            b.Property(u => u.LastName).HasMaxLength(50);
        });
        #endregion

        #region Table Relations

        #region Address
        builder.Entity<Address>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Address>()
            .HasOne(c => c.Country)
            .WithMany()
            .HasForeignKey(l => l.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Address>()
            .HasOne(c => c.AddressType)
            .WithMany()
            .HasForeignKey(l => l.AddressTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region Country
        builder.Entity<Country>()
            .HasOne(c => c.Currency)
            .WithMany()
            .HasForeignKey(l => l.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region Dictionary
        builder.Entity<Dictionary>()
            .HasOne(c => c.DictionaryType)
            .WithMany()
            .HasForeignKey(l => l.DictionaryTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region Driver
        builder.Entity<Driver>()
            .HasOne(c => c.BaseUser)
            .WithMany()
            .HasForeignKey(l => l.BaseUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Driver>()
            .HasOne(c => c.AssignedCar)
            .WithMany()
            .HasForeignKey(l => l.AssignedCarId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region Payment
        builder.Entity<Payment>()
            .HasOne(a => a.Package)
            .WithMany()
            .HasForeignKey(a => a.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region Package
        builder.Entity<Package>()
            .HasOne(a => a.Sender)
            .WithMany()
            .HasForeignKey(a => a.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Package>()
            .HasOne(a => a.Reciver)
            .WithMany()
            .HasForeignKey(a => a.ReciverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Package>()
            .HasOne(a => a.Destination)
            .WithMany()
            .HasForeignKey(a => a.DestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Package>()
            .HasOne(a => a.PackageType)
            .WithMany()
            .HasForeignKey(a => a.PackageTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Package>()
            .HasOne(a => a.PackageStatus)
            .WithMany()
            .HasForeignKey(a => a.PackageStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Package>()
            .HasOne(a => a.Payment)
            .WithMany()
            .HasForeignKey(a => a.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region StoragePackages
        builder.Entity<StoragePackages>()
            .HasOne(a => a.Package)
            .WithMany()
            .HasForeignKey(a => a.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region Transportation
        builder.Entity<Transportation>()
            .HasOne(c => c.AssignedDriver)
            .WithMany()
            .HasForeignKey(l => l.AssignedDriverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Transportation>()
            .HasOne(c => c.TransportationStatus)
            .WithMany()
            .HasForeignKey(l => l.TransportationStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Transportation>()
            .HasMany(c => c.TransportationItems)
            .WithOne(c => c.Transportation)
            .HasForeignKey(l => l.TransportationId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region TransportationItem
        builder.Entity<TransportationItem>()
            .HasOne(c => c.TransportationType)
            .WithMany()
            .HasForeignKey(l => l.TransportationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TransportationItem>()
            .HasOne(c => c.Package)
            .WithMany()
            .HasForeignKey(l => l.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
        #region User
        builder.Entity<User>()
            .HasOne(c => c.UserType)
            .WithMany()
            .HasForeignKey(l => l.UserTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #endregion
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;

                    break;
                case EntityState.Modified:
                    entry.Entity.Modified = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    public Task<int> SaveChangesWithAuditableEntityAsync(IHttpContextAccessor _httpContextAccessor, CancellationToken cancellationToken = default)
    {
        var user = _httpContextAccessor.HttpContext.User.Identity.Name;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = user;
                    entry.Entity.Created = DateTime.Now;

                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = user;
                    entry.Entity.Modified = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

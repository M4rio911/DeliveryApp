using DeliveryAppAPI.Domain.Common;
using DeliveryAppAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryAppAPI.Persistance
{
    public class DeliveryAppDbContext : DbContext
    {
        public DeliveryAppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Address> Address { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Currency> Currency { get; set; }

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
                b.Property(c => c.Short).HasMaxLength(10);
            });

            builder.Entity<User>(b =>
            {
                b.Property(u => u.FirstName).HasMaxLength(30);
                b.Property(u => u.LastName).HasMaxLength(50);
            });
            #endregion

            #region Table Relations

            #region Country
            builder.Entity<Country>()
                .HasOne(c => c.Currency)
                .WithMany()
                .HasForeignKey(l => l.CurrencyId);
            #endregion
            #region Address
            builder.Entity<Address>()
                .HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId);

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
}

using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace DeliveryApp.Infrastructure.Repositories
{
    public class PaymentRepository : DeliveryDbContextFactory, IPaymentRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbContextOptions<DeliveryDbContext> _options;
        public PaymentRepository(DbContextOptions<DeliveryDbContext> options, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            var context = CreateNewInstance(_options);

            await context.AddAsync(payment);
            await context.SaveChangesWithAuditableEntityAsync(_httpContextAccessor);

            return payment;
        }
    }
}

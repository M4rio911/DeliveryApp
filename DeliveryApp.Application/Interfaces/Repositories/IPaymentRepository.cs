using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<Payment> AddPaymentAsync(Payment payment);
}

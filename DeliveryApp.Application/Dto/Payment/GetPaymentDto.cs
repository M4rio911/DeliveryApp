using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance.Models;

namespace DeliveryApp.Application.Dto.Payments;

public class GetPaymentDto
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public int PaymentTypeId { get; set; }
    public string PaymentType { get; set; }
    public int PaymentStatusId { get; set; }
    public string PaymentStatus { get; set; }
}

namespace DeliveryApp.Application.Dto.Payments;

public class GetPaymentDto
{
    public int Id { get; set; }
    public int PaymentTypeId { get; set; }
    public int PaymentStatusId { get; set; }
    public int CurrencyId { get; set; }
    public decimal Price { get; set; }
}

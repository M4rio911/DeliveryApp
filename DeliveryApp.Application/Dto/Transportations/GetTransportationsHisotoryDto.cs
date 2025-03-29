using DeliveryApp.Application.Dto.Packages;

namespace DeliveryApp.Application.Dto.Transportations;

public class GetTransportationsHistoryDto
{
    public int TransportationId { get; set; }
    public DateTime DateOfTransport {  get; set; }
    public int TransportationStatus { get; set; }
}

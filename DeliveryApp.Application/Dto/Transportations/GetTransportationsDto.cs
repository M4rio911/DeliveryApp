namespace DeliveryApp.Application.Dto.Transportations;

public class GetTransportationsDto
{
    public int TransportationId { get; set; }
    public int AssignedDriverId { get; set; }
    public DateTime DateOfTransport {  get; set; }
}

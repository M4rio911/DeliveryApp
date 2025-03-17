using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Transportations.GetTransportation;

public class GetTransportationsResponse : BaseResponse
{
    public List<GetTransportationsDto> Transportations { get; set; }

    public GetTransportationsResponse() : base(true, null) { }
    public GetTransportationsResponse(string error) : base(error) { }
}

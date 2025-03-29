using DeliveryApp.Application.Dto.Payments;
using DeliveryApp.Application.Dto.Transportations;
using DeliveryApp.Application.Handlers.BaseModel;

namespace DeliveryApp.Application.Handlers.Transportations.GetTransportationsHistory;

public class GetTransportationsHistoryResponse : BaseResponse
{
    public List<GetTransportationsHistoryDto> Transportations { get; set; }

    public GetTransportationsHistoryResponse() : base(true, null) { }
    public GetTransportationsHistoryResponse(string error) : base(error) { }
}

using Newtonsoft.Json;

namespace DeliveryApp.Application.Handlers.BaseModel;

public class BaseResponse
{
    [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Success { get; set; }

    [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Errors { get; set; }

    public BaseResponse(bool? success, List<string> errors)
    {
        Success = success;
        Errors = errors;
    }
    public BaseResponse(List<string> errors)
    {
        Success = false;
        Errors = errors;
    }
    public BaseResponse(string error)
    {
        Success = false;
        Errors = [error];
    }
}

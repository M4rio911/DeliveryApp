using DeliveryApp.Application.Dto.Users;
using DeliveryApp.Application.Handlers.BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Handlers.Users.GetAllUsers
{
    public class GetAllUsersResponse : BaseResponse
    {

        [JsonProperty("users")]
        public List<GetUserDto> Users { get; set; }
        public GetAllUsersResponse() : base(true, null) { }
    }
}

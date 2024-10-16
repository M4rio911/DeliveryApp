﻿using DeliveryApp.Application.Handlers.BaseModel;
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
        public List<UserDto> Users { get; set; }
        public GetAllUsersResponse() : base(true, null) { }
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool ActiveStatus { get; set; }
    }
}

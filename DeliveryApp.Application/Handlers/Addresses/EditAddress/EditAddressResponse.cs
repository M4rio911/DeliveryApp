using DeliveryApp.Application.Dto.Addresses;
using DeliveryApp.Application.Handlers.BaseModel;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Handlers.Addresses.EditAddress;

public class EditAddressResponse : BaseResponse
{
    public EditAddressDto Address { get; set; }
    public EditAddressResponse(Address address) : base(true, null, true) 
    {
        Address = new EditAddressDto
        {
            AddressId = address.Id,
            Name = address.Name,
            CountryId = address.CountryId,
            PostCode = address.PostCode,
            City = address.City,
            Street = address.Street,
            Number = address.Number,
            AddressTypeId = address.AddressTypeId,
        };
    }
    public EditAddressResponse(string error) : base(error) { }
}

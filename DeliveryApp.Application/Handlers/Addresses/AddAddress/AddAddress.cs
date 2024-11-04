using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.Addresses.AddAddress;

public class AddAddress : ICommand<AddAddressResponse>
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public int? CountryId { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public int AddressTypeId { get; set; }

    public AddAddress(AddAddressParameters parameters)
    {
        UserId = parameters.UserId;
        Name = parameters.Name;
        CountryId = parameters.CountryId;
        PostCode = parameters.PostCode;
        City = parameters.City;
        Street = parameters.Street;
        Number = parameters.Number;
        AddressTypeId = parameters.AddressTypeId;
    }
}
namespace DeliveryApp.Application.Handlers.Addresses.AddAddress;

public class AddAddressParameters
{
    public string? Name { get; set; }
    public int? CountryId { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public int AddressTypeId { get; set; }
}
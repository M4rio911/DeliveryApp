namespace DeliveryApp.Application.Handlers.Addresses.EditAddress;

public class EditAddressParameters
{
    public int AddressId { get; set; }
    public string Name { get; set; }
    public int? CountryId { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public int AddressTypeId { get; set; }
}
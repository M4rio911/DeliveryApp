using DeliveryApp.Application.Handlers.Addresses.AddAddress;

namespace DeliveryApp.Application.Handlers.Packages.AddPackage;

public class AddPackageParamenters
{
    public string ReciverEmail { get; set; }
    public int? DestinationId { get; set; }
    public int PackageTypeId { get; set; }
    public int PaymentTypeId { get; set; }
    public int CurrencyId { get; set; }
    public decimal Price { get; set; }
    public AddAddressParameters? GuestAddress { get; set; }
}

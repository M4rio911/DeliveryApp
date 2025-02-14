using DeliveryApp.Application.Dto.PackagePrices;
using static DeliveryApp.Application.Interfaces.Mediator.ICommand;

namespace DeliveryApp.Application.Handlers.PackagePrices.EditPackagePrices;

public class EditPackagePrices : ICommand<EditPackagePricesResponse>
{
    public int CurrencyId { get; set; }
    public List<GetPackagePricesDto> PackagePrices { get; set; }

    public EditPackagePrices(EditPackagePricesParameters parameters)
    {
        CurrencyId = parameters.CurrencyId;
        PackagePrices = parameters.PackagePrices;
    }
}
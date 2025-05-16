using ProductCatalog.Admin.Mobile.Models;

namespace ProductCatalog.Admin.Mobile.Services;

public interface INavigationService
{
    Task GoToProductDetail(Int64 selectedProductId);
    Task GoToOverview();
    Task GoBack();
}

using ProductCatalog.Admin.Mobile.Models;

namespace ProductCatalog.Admin.Mobile.Services;

public class NavigationService : INavigationService
{
    public async Task GoToProductDetail(Int64 id)
    {
        var parameters = new Dictionary<string, object> { {"ProductId", id}};
        await Shell.Current.GoToAsync("product", parameters);
    }

    public Task GoToOverview()
        => Shell.Current.GoToAsync("//overview");

    public Task GoBack()
        => Shell.Current.GoToAsync("..");
}

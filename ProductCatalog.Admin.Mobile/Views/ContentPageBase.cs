using ProductCatalog.Admin.Mobile.ViewModels.Base;

namespace ProductCatalog.Admin.Mobile.Views;

public class ContentPageBase : ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is not IViewModelBase ivmb)
        {
            return;
        }

        await ivmb.InitializeAsyncCommand.ExecuteAsync(null);
    }
}

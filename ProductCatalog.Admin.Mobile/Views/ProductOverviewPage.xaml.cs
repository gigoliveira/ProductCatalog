using ProductCatalog.Admin.Mobile.ViewModels;

namespace ProductCatalog.Admin.Mobile.Views;

public partial class ProductOverviewPage : ContentPageBase
{
	public ProductOverviewPage(ProductListOverviewViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
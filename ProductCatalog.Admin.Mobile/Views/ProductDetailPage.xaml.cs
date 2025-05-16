using ProductCatalog.Admin.Mobile.ViewModels;

namespace ProductCatalog.Admin.Mobile.Views;

public partial class ProductDetailPage : ContentPageBase
{
	public ProductDetailPage(ProductDetailViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}
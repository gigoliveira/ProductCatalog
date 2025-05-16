using ProductCatalog.Admin.Mobile.Views;

namespace ProductCatalog.Admin.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("product", typeof(ProductDetailPage));
    }
}

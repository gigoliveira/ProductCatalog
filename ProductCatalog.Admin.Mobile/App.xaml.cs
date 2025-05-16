using ProductCatalog.Admin.Mobile.Views;

namespace ProductCatalog.Admin.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}

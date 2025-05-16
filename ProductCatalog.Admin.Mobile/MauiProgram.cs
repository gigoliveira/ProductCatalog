using CommunityToolkit.Maui;
using ProductCatalog.Admin.Mobile.Repositories;
using ProductCatalog.Admin.Mobile.Services;
using ProductCatalog.Admin.Mobile.ViewModels;
using ProductCatalog.Admin.Mobile.Views;
using Microsoft.Extensions.Logging;
using ProductCatalog.Admin.Mobile.Helpers;

namespace ProductCatalog.Admin.Mobile;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
            })
            .RegisterRepositories()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
    {
        var baseUrl = "https://fakestoreapi.com";

        builder.Services.AddHttpClient("ProductsCatalogAdminApiClient",
            client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

            });

        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        return builder;
    }

    private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();
        builder.Services.AddSingleton<IFavoriteCacheService, FavoriteCacheService>();

        return builder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ProductListOverviewViewModel>();
        builder.Services.AddTransient<ProductDetailViewModel>();
        
        return builder;
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ProductOverviewPage>();
        builder.Services.AddTransient<ProductDetailPage>();
        
        return builder;
    }
}

using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductCatalog.Admin.Mobile.Helpers;
using ProductCatalog.Admin.Mobile.Messages;
using ProductCatalog.Admin.Mobile.Models;
using ProductCatalog.Admin.Mobile.Services;
using ProductCatalog.Admin.Mobile.ViewModels.Base;

namespace ProductCatalog.Admin.Mobile.ViewModels;

public partial class ProductListOverviewViewModel : ViewModelBase
{
    private readonly IProductService _productService;
    private readonly INavigationService _navigationService;
    private readonly IFavoriteCacheService _favoriteCacheService;

    [ObservableProperty] private ObservableCollection<ProductListItemViewModel> _products = new();

    [ObservableProperty] private ProductListItemViewModel? _selectedProduct;

    [RelayCommand]
    private async Task NavigateToSelectedDetail()
    {
        if (SelectedProduct is not null)
        {
            await _navigationService.GoToProductDetail(SelectedProduct.Id);
            SelectedProduct = null;
        }
    }

    public ProductListOverviewViewModel(
        IProductService productService, 
        INavigationService navigationService,
        IFavoriteCacheService favoriteCacheService)
        {
            _productService = productService;
            _navigationService = navigationService;
            _favoriteCacheService = favoriteCacheService;
        }

    public override async Task LoadAsync()
    {
        if (Products.Count == 0)
        {
            await Loading(GetProducts);
        }
    }

    private async Task GetProducts()
    {
        List<ProductModel> products = await _productService.GetProducts();
        List<ProductListItemViewModel> listItems = new();
        foreach (var @product in products)
        {
            listItems.Add(MapProductModelToProductListItemViewModel(@product));
        }

        Products.Clear();
        Products = listItems.ToObservableCollection();
    }
    private ProductListItemViewModel MapProductModelToProductListItemViewModel(ProductModel product)
    {
        var rating = new RatingViewModel
        {
            Rate = product.Rating.Rate,
            Count = product.Rating.Count,
        };

        var cachedFavorite = _favoriteCacheService.GetFavorite(product.Id);

        return new ProductListItemViewModel(
            product.Id,
            product.Title,
            product.Price,
            product.Description,
            product.Category,
            product.Image,
            cachedFavorite, 
            rating);
    }
}

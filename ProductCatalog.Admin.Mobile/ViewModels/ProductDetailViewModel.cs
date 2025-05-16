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

public partial class ProductDetailViewModel : ViewModelBase, IQueryAttributable
{
    private readonly IProductService _productService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly IFavoriteCacheService _favoriteCacheService;


    [ObservableProperty]
    private Int64 _id;

    [ObservableProperty]
    private string _title = default!;

    [ObservableProperty]
    private double _price;

    [ObservableProperty]
    private string _image;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(FavoriteProductCommand))]
    [NotifyPropertyChangedFor(nameof(FavoriteIcon))]
    [NotifyPropertyChangedFor(nameof(FavoriteColor))]
    [NotifyPropertyChangedFor(nameof(FavoriteButtonText))]
    private bool _favorite;
    public string FavoriteIcon => Favorite ? "★" : "☆";
    public Color FavoriteColor => Favorite ? Colors.Gold : Colors.Black;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _category;

    [ObservableProperty]
    private RatingViewModel _rating = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowThumbnailImage))]
    private bool _showLargerImage;
    public bool ShowThumbnailImage => !ShowLargerImage;
    public string FavoriteButtonText => Favorite ? "Unfavorite" : "Favorite";

    [RelayCommand]
    private async Task FavoriteProduct()
    {
        Favorite = !Favorite;
        WeakReferenceMessenger.Default.Send(new FavoriteChangedMessage(Id, Favorite));
    }

    public ProductDetailViewModel(
        IProductService productService, 
        INavigationService navigationService, 
        IDialogService dialogService,
        IFavoriteCacheService favoriteCacheService)
        {
            _productService = productService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _favoriteCacheService = favoriteCacheService;
        }

    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                if (Id != 0)
                {
                    await GetProduct(Id);
                }
            });
    }

    private async Task GetProduct(Int64 id)
    {
        var @product = await _productService.GetProduct(id);

        if (@product != null)
        {
            MapProductData(@product);
        }
    }

    private void MapProductData(ProductModel @product)
    {
        Id = @product.Id;
        Title = @product.Title;
        Price = @product.Price;
        Image = @product.Image;
        Description = @product.Description;
        Category = @product.Category;
        Rating = new RatingViewModel
        {
            Rate = @product.Rating.Rate,
            Count = @product.Rating.Count,
        };
    }
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var productId = query["ProductId"].ToString();
        if (Int64.TryParse(productId, out var selectedId))
        {
            Id = selectedId;
        }
    }
    partial void OnIdChanged(long value)
    {
        LoadFavoriteStatus();
    }
    private void LoadFavoriteStatus()
    {
        Favorite = _favoriteCacheService.GetFavorite(Id);
    }

    partial void OnFavoriteChanged(bool value)
    {
        _favoriteCacheService.SetFavorite(Id, value);
    }
}

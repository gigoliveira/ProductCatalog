using Moq;
using Xunit;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using ProductCatalog.Admin.Mobile.ViewModels;
using ProductCatalog.Admin.Mobile.Services;
using ProductCatalog.Admin.Mobile.Models;
using NSubstitute;
using ProductCatalog.Admin.Mobile.Helpers;
using ProductCatalog.Admin.Mobile.Messages;

namespace ProductCatalog.Admin.Mobile.Tests.ViewModels;
public class ProductDetailViewModelTests
{
    private readonly Mock<IProductService> _productServiceMock = new();
    private readonly Mock<INavigationService> _navigationServiceMock = new();
    private readonly Mock<IDialogService> _dialogServiceMock = new();
    private readonly Mock<IFavoriteCacheService> _favoriteCacheServiceMock = new();

    private ProductDetailViewModel CreateViewModel()
    {
        var viewModel = new ProductDetailViewModel(
            _productServiceMock.Object,
            _navigationServiceMock.Object,
            _dialogServiceMock.Object, _favoriteCacheServiceMock.Object );

        return viewModel;
    }
    public ProductDetailViewModelTests()
    {
        // Setup default behavior for favorite cache
        _favoriteCacheServiceMock.Setup(f => f.GetFavorite(It.IsAny<long>())).Returns(false);
        _favoriteCacheServiceMock.Setup(f => f.SetFavorite(It.IsAny<long>(), It.IsAny<bool>()));
    }

    [Fact]
    public async Task LoadAsync_WithValidId_ShouldLoadProductAndFavorite()
    {
        // Arrange
        var product = new ProductModel
        {
            Id = 1,
            Title = "Test Product",
            Price = 10.0,
            Image = "image.jpg",
            Description = "desc",
            Category = "category",
            Rating = new RatingModel{ Rate = 4.5, Count = 10 }
        };

        _productServiceMock.Setup(p => p.GetProduct(1)).ReturnsAsync(product);

        var vm = new ProductDetailViewModel(
            _productServiceMock.Object,
            _navigationServiceMock.Object,
            _dialogServiceMock.Object,
            _favoriteCacheServiceMock.Object);

        vm.Id = 1;

        // Act
        await vm.LoadAsync();

        // Assert
        Assert.Equal(product.Id, vm.Id);
        Assert.Equal(product.Title, vm.Title);
        Assert.Equal(product.Price, vm.Price);
        Assert.Equal(product.Image, vm.Image);
        Assert.Equal(product.Description, vm.Description);
        Assert.Equal(product.Category, vm.Category);
        Assert.Equal(product.Rating.Rate, vm.Rating.Rate);
        Assert.Equal(product.Rating.Count, vm.Rating.Count);

        _favoriteCacheServiceMock.Verify(f => f.GetFavorite(1), Times.Once);
    }

    [Fact]
    public void FavoriteProductCommand_TogglesFavoriteAndSendsMessage()
    {
        // Arrange
        var vm = new ProductDetailViewModel(
            _productServiceMock.Object,
            _navigationServiceMock.Object,
            _dialogServiceMock.Object,
            _favoriteCacheServiceMock.Object);

        vm.Id = 1;
        vm.Favorite = false;

        FavoriteChangedMessage? sentMessage = null;
        WeakReferenceMessenger.Default.Register<FavoriteChangedMessage>(this, (r, m) => sentMessage = m);

        // Act
        vm.FavoriteProductCommand.Execute(null);

        // Assert
        Assert.True(vm.Favorite);
        Assert.NotNull(sentMessage);
        Assert.Equal(1, sentMessage.ProductId);
        Assert.True(sentMessage.Favorite);

        // Clean up messenger registration
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    [Fact]
    public void FavoriteProductCommand_AlwaysCanExecute()
    {
        // Arrange
        var vm = CreateViewModel();

        // Assert
        Assert.True(vm.FavoriteProductCommand.CanExecute(null));
    }

    [Fact]
    public void OnFavoriteChanged_ShouldCallSetFavorite()
    {
        // Arrange
        var vm = CreateViewModel();
        vm.Id = 1;

        // Act
        vm.Favorite = true;

        // Assert
        _favoriteCacheServiceMock.Verify(f => f.SetFavorite(1, true), Times.Once);
    }

    [Fact]
    public void ApplyQueryAttributes_ValidProductId_SetsId()
    {
        // Arrange
        var vm = CreateViewModel();
        var query = new Dictionary<string, object> { ["ProductId"] = "5" };

        // Act
        vm.ApplyQueryAttributes(query);

        // Assert
        Assert.Equal(5, vm.Id);
        _favoriteCacheServiceMock.Verify(f => f.GetFavorite(5), Times.Once);
    }

    [Theory]
    [InlineData(true, "Unfavorite", "★")]
    [InlineData(false, "Favorite", "☆")]
    public void Favorite_Changes_ReflectOnUIProperties(bool favorite, string expectedText, string expectedIcon)
    {
        // Arrange
        var vm = CreateViewModel();

        // Act
        vm.Favorite = favorite;

        // Assert
        Assert.Equal(expectedText, vm.FavoriteButtonText);
        Assert.Equal(expectedIcon, vm.FavoriteIcon);
    }

}

using System;
using CommunityToolkit.Mvvm.Messaging;
using ProductCatalog.Admin.Mobile.Messages;
using ProductCatalog.Admin.Mobile.ViewModels;
using Xunit;

namespace ProductCatalog.Admin.Mobile.Tests.ViewModels;
public class ProductListItemViewModelTests
{
    [Fact]
    public void Constructor_PropertiesAreSet_AndRegistersToMessenger()
    {
        // Arrange
        var id = 1L;
        var title = "Product 1";
        var price = 10.0;
        var description = "Description 1";
        var category = "Category 1";
        var image = "ImageUrl";
        var favorite = true;

        // Act
        var vm = new ProductListItemViewModel(id, title, price, description, category, image, favorite);

        // Assert
        Assert.Equal(id, vm.Id);
        Assert.Equal(title, vm.Title);
        Assert.Equal(price, vm.Price);
        Assert.Equal(description, vm.Description);
        Assert.Equal(category, vm.Category);
        Assert.Equal(image, vm.Image);
        Assert.Equal(favorite, vm.Favorite);

        // Check if messenger registered (indirect)
        Assert.True(WeakReferenceMessenger.Default.IsRegistered<FavoriteChangedMessage>(vm));
    }

    [Fact]
    public void Receive_UpdatesFavorite_WhenMessageForSameId()
    {
        // Arrange
        var id = 1L;
        var vm = new ProductListItemViewModel(id, "Title", 5, "Desc", "Cat", "Img", false);

        // Act
        WeakReferenceMessenger.Default.Send(new FavoriteChangedMessage(id, true));

        // Assert
        Assert.True(vm.Favorite);
    }

    [Fact]
    public void Receive_DoesNotUpdateFavorite_WhenMessageForDifferentId()
    {
        // Arrange
        var id = 1L;
        var vm = new ProductListItemViewModel(id, "Title", 5, "Desc", "Cat", "Img", false);

        // Act
        WeakReferenceMessenger.Default.Send(new FavoriteChangedMessage(999, true));

        // Assert
        Assert.False(vm.Favorite);
    }

    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        // Arrange
        var rating = new RatingViewModel { Rate = 4.2, Count = 100 };
        var vm = new ProductListItemViewModel(1, "Title", 20, "Desc", "Cat", "img.jpg", true, rating);

        // Assert
        Assert.Equal(1, vm.Id);
        Assert.Equal("Title", vm.Title);
        Assert.Equal("Desc", vm.Description);
        Assert.Equal("Cat", vm.Category);
        Assert.Equal("img.jpg", vm.Image);
        Assert.True(vm.Favorite);
        Assert.Equal(rating, vm.Rating);
    }

    [Fact]
    public void Receive_MessageWithDifferentId_DoesNotUpdateFavorite()
    {
        // Arrange
        var vm = new ProductListItemViewModel(1, "Title", 20, "Desc", "Cat", "img.jpg", false);

        // Act
        WeakReferenceMessenger.Default.Send(new FavoriteChangedMessage(999, true));

        // Assert
        Assert.False(vm.Favorite);
    }

}

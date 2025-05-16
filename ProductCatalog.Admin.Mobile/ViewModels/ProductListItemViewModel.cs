using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ProductCatalog.Admin.Mobile.Messages;

namespace ProductCatalog.Admin.Mobile.ViewModels;

public partial class ProductListItemViewModel : ObservableObject, IRecipient<FavoriteChangedMessage>
{
    [ObservableProperty]
    private Int64 _id;

    [ObservableProperty]
    private string _title = default!;

    [ObservableProperty]
    private double _price;

    [ObservableProperty]
    private string _description = default!;

    [ObservableProperty]
    private string _category = default!;

    [ObservableProperty]
    private string _image;

    [ObservableProperty]
    private RatingViewModel? _rating;

    [ObservableProperty]
    private bool _favorite;

    public ProductListItemViewModel(
        Int64 id,
        string title,
        double price,
        string description,
        string category,
        string image,
        bool favorite,
        RatingViewModel? rating = null)
    {
        Id = id;
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Favorite = favorite;
        Rating = rating;

        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(FavoriteChangedMessage message)
    {
        if (message.ProductId == Id)
        {
            Favorite = message.Favorite;
        }
    }
}

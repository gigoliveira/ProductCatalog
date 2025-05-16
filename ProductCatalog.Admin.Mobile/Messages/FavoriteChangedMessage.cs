using ProductCatalog.Admin.Mobile.ViewModels;

namespace ProductCatalog.Admin.Mobile.Messages;

public class FavoriteChangedMessage
{
    public Int64 ProductId { get; }
    public bool Favorite { get; }

    public FavoriteChangedMessage(
        Int64 id,
        bool favorite)
    {
        ProductId = id;
        Favorite = favorite;
    }
}

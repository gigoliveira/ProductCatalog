namespace ProductCatalog.Admin.Mobile.Models;

public class ProductModel
{
    public Int64 Id { get; set; }
    public string Title { get; set; } = default!;
    public double Price { get; set; }
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Image { get; set; }
    public RatingModel Rating { get; set; } = default!;
    public bool Favorite { get; set; }

}

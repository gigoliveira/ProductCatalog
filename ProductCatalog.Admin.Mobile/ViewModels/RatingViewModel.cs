using CommunityToolkit.Mvvm.ComponentModel;

namespace ProductCatalog.Admin.Mobile.ViewModels;

public partial class RatingViewModel : ObservableObject
{
    [ObservableProperty]
    private Double _rate;

    [ObservableProperty]
    private Int64 _count = default!;

}

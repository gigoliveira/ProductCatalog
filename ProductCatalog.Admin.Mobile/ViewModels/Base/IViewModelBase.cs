using CommunityToolkit.Mvvm.Input;

namespace ProductCatalog.Admin.Mobile.ViewModels.Base;

public interface IViewModelBase
{
    IAsyncRelayCommand InitializeAsyncCommand { get; }
}

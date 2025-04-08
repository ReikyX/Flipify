using System.Windows.Input;

namespace Flipify.ViewModel;

public partial class DeckVM : BaseVM
{
    public ICommand NavigateBackCommand { get; }
    public DeckVM()
    {
        NavigateBackCommand = new Command(NavigateBack);
    }
    private async void NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}

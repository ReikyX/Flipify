using System.Windows.Input;

namespace Flipify.ViewModel;

public partial class MainViewModel : BaseVM
{

    public ICommand Beenden { get; }

    public MainViewModel()
    {
        Beenden = new Command(Application.Current.Quit);
    }

}

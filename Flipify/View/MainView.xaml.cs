using Flipify.ViewModel;

namespace Flipify.View;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel mvm)
	{
		InitializeComponent();
		BindingContext = mvm;
	}
}
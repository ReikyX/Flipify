using Flipify.Model;
using Flipify.ViewModel;

namespace Flipify.View;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}
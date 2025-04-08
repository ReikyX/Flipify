using Flipify.Model;
using Flipify.Service;
using Flipify.ViewModel;
namespace Flipify.View;

public partial class AddDeckView : ContentPage
{
	public AddDeckView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
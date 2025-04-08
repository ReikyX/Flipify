using Flipify.Model;
using Flipify.Service;
using Flipify.ViewModel;
namespace Flipify.View;

public partial class AddDeckView : ContentPage
{
	public AddDeckView(AddDeckVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
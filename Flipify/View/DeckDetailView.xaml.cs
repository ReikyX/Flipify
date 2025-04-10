using Flipify.Model;
using Flipify.Service;
using Flipify.ViewModel;
namespace Flipify.View;

public partial class DeckDetailView : ContentPage
{
	public DeckDetailView(DeckDetailVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
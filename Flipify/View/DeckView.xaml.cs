using Flipify.ViewModel;

namespace Flipify.View;
public partial class DeckView : ContentPage
{
	public DeckView(DeckVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
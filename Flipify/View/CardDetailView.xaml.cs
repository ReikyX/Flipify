using Flipify.ViewModel;

namespace Flipify.View;

public partial class CardDetailView : ContentPage
{
	public CardDetailView(CardDetailVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
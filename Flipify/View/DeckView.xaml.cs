
using Flipify.ViewModel;

namespace Flipify.View;

public partial class DeckView : ContentPage
{
	public DeckView(DeckVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
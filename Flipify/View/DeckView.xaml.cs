
using Flipify.Model;
using Flipify.ViewModel;

namespace Flipify.View;

public partial class DeckView : ContentPage
{
    private DeckVM _vm;
    public DeckView(DeckVM vm)
    {

        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;

    }

    private async void RotateImage(object sender, TappedEventArgs e)
    {
        var grid = sender as Grid;
        var card = grid.BindingContext as Card;

        if (card == null) return;

        var image = grid.FindByName<Image>("CardImage");
        var label = grid.FindByName<Label>("CardText");

        if (card.IsFlipped)
        {
            await image.RotateYTo(0, 500, Easing.CubicInOut);
            label.Text = card.Front;
        }
        else
        {
            await image.RotateYTo(180, 500, Easing.CubicInOut);
            label.Text = card.Back;
        }

        card.IsFlipped = !card.IsFlipped;
    }
}
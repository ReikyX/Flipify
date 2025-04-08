using Flipify.ViewModel;
using System.Collections.ObjectModel;

namespace Flipify.Model;

public class Deck
{
    public string DeckTitle { get; set; }
    public ObservableCollection<Card> Cards { get; set; } = new();

}

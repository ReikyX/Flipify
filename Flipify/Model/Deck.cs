using Flipify.ViewModel;
using System.Collections.ObjectModel;

namespace Flipify.Model;

public partial class Deck : BaseVM
{
    private string _deckTitle;
    public string DeckTitle
    {
        get => _deckTitle;
        set
        {
            if (_deckTitle != value)
            {
                _deckTitle = value;
                OnPropertyChanged(nameof(DeckTitle));
            }
        }
    }
    public ObservableCollection<Card> Cards { get; set; } = new();

    public bool DeckIsEditable { get; set; } = true;
}

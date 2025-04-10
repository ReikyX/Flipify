using Flipify.Model;
using Flipify.Service;
using Flipify.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flipify.ViewModel;
[QueryProperty(nameof(SelectedDeck), "SelectedDeck")]
public partial class DeckVM : BaseVM
{
    private DeckService _deckService;
    public string CurrentQuestion { get; set; }
    public string CurrentAnswer { get; set; }



    private Card _selectedCard;
    public Card SelectedCard
    {
        get => _selectedCard;
        set
        {
            _selectedCard = value;
            OnPropertyChanged(nameof(SelectedCard));
            OnPropertyChanged(nameof(CanEditDeck));
        }
    }

    private Deck _selectedDeck;
    public Deck SelectedDeck
    {
        get => _selectedDeck;
        set
        {
            _selectedDeck = value;
            OnPropertyChanged(nameof(SelectedDeck));
            OnPropertyChanged(nameof(CanEditDeck));
            OnPropertyChanged(nameof(Cards));
        }
    }

    public ObservableCollection<Card> Cards => SelectedDeck?.Cards;
    public bool CanEditDeck => SelectedDeck?.DeckIsEditable ?? false;


    //public ICommand FlipCardCommand { get; }
    public ICommand NewCardCommand { get; }
    public ICommand EditCardCommand { get; }
    public ICommand DeleteCardCommand { get; }
    public ICommand NavigateBackCommand { get; }

    public DeckVM(DeckService deckService)
    {
        _deckService = deckService;
        NewCardCommand = new Command(NewCard);
        EditCardCommand = new Command<Card>(EditCard);
        DeleteCardCommand = new Command<Card>(DeleteCard);
        NavigateBackCommand = new Command(NavigateBack);
        //FlipCardCommand = new Command<Card>(FlipCard);
    }

    //private void FlipCard(Card card)
    //{
    //    if (card != null)
    //    {
    //        card.IsFlipped = !card.IsFlipped;
    //        OnPropertyChanged(nameof(Cards));
    //    }
    //}
    private async void NewCard()
    {
        await Shell.Current.GoToAsync(nameof(CardDetailView), true, new Dictionary<string, object>
    {
            { "EditCard" , null },
            { "SelectedDeck", SelectedDeck }
    });
    }
    private async void EditCard(Card card)
    {
        if (card == null) return;

        await Shell.Current.GoToAsync(nameof(CardDetailView), true, new Dictionary<string, object>()
        {
            { "EditCard",  card},
            { "SelectedDeck", SelectedDeck }
        });
    }
    private async void DeleteCard(Card card)
    {
        if (card == null) return;

        bool confirm = await Shell.Current.DisplayAlert("Karte löschen", "Willst du diese Karte wirklich löschen?", "Ja", "Nein");
        if (!confirm) return;

        if (SelectedDeck.Cards.Contains(card))
        {
            SelectedDeck.Cards.Remove(card);
            SelectedCard = null;
            _deckService.SaveUserDecks();
        }
    }
    private async void NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}

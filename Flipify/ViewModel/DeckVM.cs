using Flipify.Model;
using Flipify.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flipify.ViewModel;
[QueryProperty(nameof(SelectedDeck), "SelectedDeck")]
public partial class DeckVM : BaseVM
{
    private DeckService _deckService;
    public string CurrentQuestion { get; set; }
    public string CurrentAnswer { get; set; }

    private Deck _selectedDeck;
    public Deck SelectedDeck
    {
        get => _selectedDeck;
        set
        {
            _selectedDeck = value;
            OnPropertyChanged(nameof(SelectedDeck));
            OnPropertyChanged(nameof(Cards));
            OnPropertyChanged(nameof(CanEditDeck));
        }
    }
    public ObservableCollection<Card> Cards => SelectedDeck?.Cards ?? new();
    public bool CanEditDeck => SelectedDeck?.IsEditable ?? false;

    public ICommand NavigateBackCommand { get; }
    public ICommand AddCardCommand { get; }


    public DeckVM(DeckService deckService)
    {
        _deckService = deckService;
        AddCardCommand = new Command(AddCard);
        NavigateBackCommand = new Command(NavigateBack);
    }
    private async void AddCard()
    {
        if (!string.IsNullOrWhiteSpace(CurrentQuestion) && !string.IsNullOrWhiteSpace(CurrentAnswer))
        {
            var newCard = new Card
            {
                Front = CurrentQuestion,
                Back = CurrentAnswer
            };

            SelectedDeck.Cards.Add(newCard);
            _deckService.SaveUserDecks();

            CurrentQuestion = string.Empty;
            CurrentAnswer = string.Empty;

            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(CurrentAnswer));
            OnPropertyChanged(nameof(Cards));
        }
        else
        {
            await Shell.Current.DisplayAlert("Fehler", "Bitte gib Frage & Antwort ein!", "OK");
        }
    }
    private async void NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}

using Flipify.Model;
using Flipify.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flipify.ViewModel;
[QueryProperty(nameof(EditDeck), "EditDeck")]
public partial class DeckDetailVM : BaseVM
{
    private DeckService _deckService;
    private Deck _editDeck;
    public Deck EditDeck
    {
        get => _editDeck;
        set
        {
            _editDeck = value;
            if (_editDeck != null)
            {
                NewDeckTitle = _editDeck.DeckTitle;
            }
            else
            {
                NewDeckTitle = string.Empty;
            }
            OnPropertyChanged(nameof(LabelText));
            OnPropertyChanged(nameof(NewDeckTitle));
        }
    }
    public string LabelText => EditDeck != null ? "Deck bearbeiten" : "Neues Deck erstellen";
    private string _newDeckTitle;
    public string NewDeckTitle
    {
        get => _newDeckTitle;
        set
        {
            _newDeckTitle = value;
            OnPropertyChanged(nameof(NewDeckTitle));
        }
    }
    public ICommand SaveDeckCommand { get; }
    public ICommand NavigateBackCommand { get; }

    public DeckDetailVM(DeckService deckService)
    {
        NavigateBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        SaveDeckCommand = new Command(SaveDeck);

        _deckService = deckService;
    }
    private async void SaveDeck()
    {
        if (string.IsNullOrWhiteSpace(NewDeckTitle))
        {
            await Shell.Current.DisplayAlert("Fehler", "Bitte gib einen Decknamen ein!", "OK");
            return;
        }
        if (EditDeck != null)
        {
            bool exists = _deckService.UserDecks
                .Any(d => d.DeckTitle.Equals(NewDeckTitle, StringComparison.OrdinalIgnoreCase) && d != EditDeck);
            if (exists)
            {
                await Shell.Current.DisplayAlert("Fehler", "Ein Deck mit diesem Namen existiert bereits!", "OK");
                return;
            }

            EditDeck.DeckTitle = NewDeckTitle;
            _deckService.SaveUserDecks();
        }
        else
        {
            bool exists = _deckService.UserDecks.Any(d => d.DeckTitle.Equals(NewDeckTitle, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                await Shell.Current.DisplayAlert("Fehler", "Ein Deck mit diesem Namen existiert bereits!", "OK");
                return;
            }
            var newDeck = new Deck
            {
                DeckTitle = NewDeckTitle,
                Cards = new ObservableCollection<Card>(),
                DeckIsEditable = true
            };
            _deckService.AddUserDeck(newDeck);
            await Shell.Current.DisplayAlert("Erfolgreich!", "Deck wurde gespeichert!", "OK");
        }
        NewDeckTitle = string.Empty;

        await Shell.Current.GoToAsync("..");
    }
}


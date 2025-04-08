using Flipify.Model;
using Flipify.Service;
using Flipify.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flipify.ViewModel;

public partial class MainViewModel : BaseVM
{
    private DeckService _deckService;

    private ObservableCollection<Deck> _decksLeft;
    public ObservableCollection<Deck> DecksLeft
    {
        get => _decksLeft;
        set
        {
            _decksLeft = value;
            OnPropertyChanged(nameof(DecksLeft));
        }
    }

    private ObservableCollection<Deck> _decksRight;
    public ObservableCollection<Deck> DecksRight
    {
        get => _decksRight;
        set
        {
            _decksRight = value;
            OnPropertyChanged(nameof(DecksRight));
        }
    }

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

    public ICommand BeendenCommand { get; }
    public ICommand AddDeckCommand { get; }
    public ICommand OpenDeckCommand { get; }
    public ICommand DeleteDeckCommand { get; }
    public ICommand NavigateToCommand { get; }
    public ICommand NavigateToNewDeckCommand { get; }
    public ICommand NavigateBackCommand { get; }



    public MainViewModel(DeckService deckService)
    {
        BeendenCommand = new Command(Application.Current.Quit);
        AddDeckCommand = new Command(AddDeck);
        OpenDeckCommand = new Command<Deck>(OpenDeck);
        DeleteDeckCommand = new Command<Deck>(DeleteDeck);

        NavigateToCommand = new Command<Deck>(OnGridTapped);
        NavigateToNewDeckCommand = new Command<string>(async (route) => await Shell.Current.GoToAsync(route));
        NavigateBackCommand = new Command(NavigateBack);

        _deckService = deckService;

        DecksLeft = _deckService.DefaultDecks;
        DecksRight = _deckService.UserDecks;
    }

    private async void OnGridTapped(Deck selectedDeck)
    {
        await Shell.Current.GoToAsync(nameof(DeckView), true, new Dictionary<string, object>()
            {
                {
                "SelectedDeck", selectedDeck
                }

            });
    }
    private async void OpenDeck(Deck deck)
    {
        await Shell.Current.GoToAsync(nameof(DeckView));
    }
    public async void AddDeck()
    {
        if (!string.IsNullOrWhiteSpace(NewDeckTitle))
        {
            if (DecksRight.Any(d => d.DeckTitle.Equals(NewDeckTitle, StringComparison.OrdinalIgnoreCase)))
            {
                await Shell.Current.DisplayAlert("Fehler", "Deck existiert bereits!", "OK");
                return;
            }

            var newDeck = new Deck
            {
                DeckTitle = NewDeckTitle,
                Cards = new ObservableCollection<Card>()
            };

            _deckService.AddUserDeck(newDeck);
            DecksRight = _deckService.GetDecksRight();
            NewDeckTitle = string.Empty;
            await Shell.Current.DisplayAlert("Success", "Deck added successfully", "OK");
            await Shell.Current.GoToAsync(nameof(MainView));
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Please enter a deck title", "OK");
        }
    }
    public async void DeleteDeck(Deck deck)
    {
        if (deck == null) return;

        bool confirm = await Shell.Current.DisplayAlert("Deck löschen", $"Willst du das Deck '{deck.DeckTitle}' löschen?", "Ja", "Nein");

        if (!confirm) return;

        _deckService.RemoveUserDeck(deck);
        DecksRight.Remove(deck);

    }
    private async void NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}

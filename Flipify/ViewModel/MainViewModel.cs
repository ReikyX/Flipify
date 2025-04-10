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

    public ICommand BeendenCommand { get; }
    public ICommand NavigateToCommand { get; }
    public ICommand CreateNewDeckCommand { get; }
    public ICommand EditDeckCommand { get; }
    public ICommand DeleteDeckCommand { get; }

    public MainViewModel(DeckService deckService)
    {
        _deckService = deckService;

        NavigateToCommand = new Command<Deck>(OnGridTapped);
        CreateNewDeckCommand = new Command(CreateNewDeck);
        EditDeckCommand = new Command<Deck>(EditDeck);
        DeleteDeckCommand = new Command<Deck>(DeleteDeck);
        BeendenCommand = new Command(Application.Current.Quit);


        DecksLeft = _deckService.DefaultDecks;
        DecksRight = _deckService.UserDecks;
    }

    private async void OnGridTapped(Deck deck)
    {
        await Shell.Current.GoToAsync(nameof(DeckView), true, new Dictionary<string, object>()
            {
                {
                "SelectedDeck", deck
                }

            });
    }
    private async void CreateNewDeck()
    {
        await Shell.Current.GoToAsync(nameof(DeckDetailView), true, new Dictionary<string, object>
        {
            {"EditDeck", null }
        });
    }
    private async void EditDeck(Deck deck)
    {

        var param = new Dictionary<string, object>
    {
        { "EditDeck", deck }
    };

        await Shell.Current.GoToAsync(nameof(DeckDetailView), true, param);
    }
    private async void DeleteDeck(Deck deck)
    {
        if (deck == null) return;

        bool confirm = await Shell.Current.DisplayAlert("Löschen", "Deck wirklich löschen?", "Ja", "Nein");
        if (!confirm) return;

        _deckService.RemoveUserDeck(deck);

        await Shell.Current.DisplayAlert("Erfolgreich", "Deck wurde gelöscht!", "OK");
    }
}

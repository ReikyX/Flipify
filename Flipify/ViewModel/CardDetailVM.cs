using Flipify.Model;
using Flipify.Service;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Flipify.ViewModel;

[QueryProperty(nameof(EditCard), "EditCard")]
[QueryProperty(nameof(SelectedDeck), "SelectedDeck")]
public partial class CardDetailVM : BaseVM
{
    private DeckService _deckService;
    private Card _editCard;
    public Card EditCard
    {
        get => _editCard;
        set
        {
            _editCard = value;
            if (value != null)
            {
                Front = value.Front;
                Back = value.Back;
            }
            else
            {
                Front = string.Empty;
                Back = string.Empty;
            }
            OnPropertyChanged(nameof(EditCard));
            OnPropertyChanged(nameof(LabelText));
            OnPropertyChanged(nameof(Front));
            OnPropertyChanged(nameof(Back));
        }
    }
    public Deck SelectedDeck { get; set; }

    public ImageSource LabelText => EditCard != null ? "editcard.png" : "newcard.png";

    private string _front;
    public string Front
    {
        get => _front;
        set
        {
            _front = value;
            OnPropertyChanged(nameof(Front));
        }
    }

    private string _back;
    public string Back
    {
        get => _back;
        set
        {
            _back = value;
            OnPropertyChanged(nameof(Back));
        }
    }

    public ICommand SaveCardCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public CardDetailVM(DeckService deckService)
    {
        _deckService = deckService;
        SaveCardCommand = new Command(SaveCard);
        NavigateBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async void SaveCard()
    {
        if (string.IsNullOrWhiteSpace(Front) || string.IsNullOrWhiteSpace(Back))
        {
            await Shell.Current.DisplayAlert("Fehler", "Beide Felder müssen ausgefüllt sein", "OK");
            return;
        }
        if (EditCard != null)
        {
            EditCard.Front = Front;
            EditCard.Back = Back;
            EditCard.OnPropertyChanged(nameof(Card.DisplayText));
        }
        else
        {
            var newCard = new Card { Front = Front, Back = Back };
            SelectedDeck?.Cards.Add(newCard);
        }
        await Shell.Current.DisplayAlert("Erfolgreich!", "Karte wurde gespeichert!", "OK");
        _deckService.SaveUserDecks();
        
        await Shell.Current.GoToAsync("..");
    }
}

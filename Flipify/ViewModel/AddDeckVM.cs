using Flipify.Model;
using Flipify.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flipify.ViewModel;

public partial class AddDeckVM : BaseVM
{
    private DeckService _deckService;

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
    public ICommand AddDeckCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public AddDeckVM(DeckService deckService)
    {
        NavigateBackCommand = new Command(NavigateBack);
        AddDeckCommand = new Command(AddDeck);

        _deckService = deckService;
    }
    private async void NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }
    private async void AddDeck()
    {
        if (!string.IsNullOrWhiteSpace(NewDeckTitle))
        {
            var newDeck = new Deck
            {
                DeckTitle = NewDeckTitle,
                Cards = new ObservableCollection<Card>()
            };

            _deckService.AddUserDeck(newDeck);
            NewDeckTitle = string.Empty;

            await Shell.Current.DisplayAlert("Erfolgreich!", "Deck wurde gespeichert!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Fehler", "Bitte gib einen Decknamen ein!", "OK");
        }
    }
}

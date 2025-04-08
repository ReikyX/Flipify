using Flipify.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Flipify.Service;

public class DeckService
{
    public ObservableCollection<Deck> DefaultDecks { get; set; } = new();
    public ObservableCollection<Deck> UserDecks { get; set; } = new();
    public DeckService()
    {
        LoadDefaultDecks();
        LoadUserDecks();
    }

    public void AddUserDeck(Deck deck)
    {
        UserDecks.Add(deck);
        SaveUserDecks();
    }
    public void RemoveUserDeck(Deck deck)
    {
        UserDecks.Remove(deck);
        SaveUserDecks();
    }
    public void SaveUserDecks()
    {
            string json = JsonSerializer.Serialize(UserDecks);
            File.WriteAllText("decks.json", json);
    }
    public void LoadUserDecks()
    {
        if (File.Exists("decks.json"))
        {
            try
            {
                string json = File.ReadAllText("decks.json");
                var loaded = JsonSerializer.Deserialize<ObservableCollection<Deck>>(json);

                UserDecks = loaded ?? new ObservableCollection<Deck>();

            }
            catch
            {
                UserDecks = new ObservableCollection<Deck>();
            }
        }
    }
    public void LoadDefaultDecks()
    {
        DefaultDecks = new ObservableCollection<Deck>
        {
            new Deck
            {
                DeckTitle = "Englisch A1",
                Cards = new ObservableCollection<Card>
                {
                    new Card { Front = "Apple", Back = "Apfel" },
                    new Card { Front = "Car", Back = "Auto" }
                }
            },
            new Deck
            {
                DeckTitle = "Französisch Basics",
                Cards = new ObservableCollection<Card>
                {
                    new Card { Front = "Bonjour", Back = "Hallo" },
                    new Card { Front = "Merci", Back = "Danke" }
                }
            }
        };
    }

    public ObservableCollection<Deck> GetDecksRight()
    {
        return UserDecks;
    }
    public ObservableCollection<Deck> GetDecksLeft()
    {
        return DefaultDecks;
    }
}


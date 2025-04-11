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
                DeckTitle = "C#",
                DeckIsEditable = false,
                Cards = new ObservableCollection<Card>
                {
                    new Card { Front = "Was ist C# ?", Back = "C# ist eine objektorientierte Programmiersprache von Microsoft, hauptsächlich für .NET-Anwendungen." },
                    new Card { Front = "Werttypen vs. Referenztypen", Back = "Werttypen (z.B. int): Speichern den Wert direkt.\n\nReferenztypen (z.B. class): Speichern die Referenz (Speicheradresse) auf das Objekt." },
                    new Card {Front = "==   vs.   Equals()", Back = "== : Vergleicht Werte (für primitive Typen) oder Referenzen.\n\nEquals() : Vergleicht den Inhalt von Objekten (überschreibbar)."},
                    new Card {Front = "Was ist eine Klasse?" , Back = "Eine Vorlage für Objekte, die Felder und Methoden enthält." },
                    new Card {Front = "Was sind Eigenschaften?" , Back = "Eigenschaften kapseln Felder und ermöglichen sicheren Zugriff (mit Getter/Setter)." },
                    new Card {Front = "Was ist der Unterschied zwischen public, private und protected?" , Back = "public: Zugriff überall.\n\nprivate: Zugriff nur innerhalb der Klasse.\n\nprotected: Zugriff innerhalb der Klasse und abgeleiteter Klassen." },
                    new Card {Front = "Was ist eine Methode?" , Back = "Eine Funktion in einer Klasse, die Verhalten oder Aktionen beschreibt." },
                    new Card {Front = "Was ist der Konstruktor?" , Back = "Eine spezielle Methode, die beim Erstellen eines Objekts aufgerufen wird, um es zu initialisieren." },


                }
            },
            new Deck
            {
                DeckTitle = "Python",
                DeckIsEditable = false,
                Cards = new ObservableCollection<Card>
                {
                    new Card { Front = "Was ist Python?", Back = "Hochgradig lesbare, dynamisch typisierte, objektorientierte Programmiersprache." },
                    new Card { Front = "Datentypen", Back = "int\tfloat\tstr\n bool\tlist\tdict\nset\ttuple" },
                    new Card { Front = "Listen vs. Tupel", Back = "list: veränderbar ([])\n\ntuple: unveränderbar (())" },
                    new Card { Front = "if-else Syntax", Back = "if x > 0:\r\n    print(\"Positiv\")\r\nelse:\r\n    print(\"Negativ\")" },
                    new Card { Front = "Funktion definieren", Back = "def sag_hallo(name):\r\n    return \"Hallo \" + name" },
                    new Card { Front = "Schleifen", Back = "for i in range(5):  \r\n    print(i)\r\n\r\nwhile x < 10:  \r\n    x += 1" },
                    new Card { Front = "Dictionary", Back = "person = {\"name\": \"Max\", \"alter\": 25}\r\nprint(person[\"name\"])" },
                    new Card { Front = "Klasse", Back = "class Auto:\r\n    def __init__(self, marke):\r\n        self.marke = marke" }
                }
            },
            new Deck
            {
                DeckTitle = "Sql",
                DeckIsEditable = false,
                Cards = new ObservableCollection<Card>
                {
                    new Card { Front = "Was ist SQL?", Back = "Structured Query Language\n\nSprache zur Verwaltung von Daten in relationalen DBs." },
                    new Card { Front = "SELECT-Abfrage", Back = "SELECT * FROM kunden;" },
                    new Card { Front = "WHERE-Klausel", Back = "SELECT * FROM kunden WHERE land = 'DE';" },
                    new Card { Front = "INSERT", Back = "INSERT INTO kunden (name, land) VALUES ('Anna', 'DE');" },
                    new Card { Front = "UPDATE", Back = "UPDATE kunden SET land = 'AT' WHERE id = 1;" },
                    new Card { Front = "DELETE", Back = "DELETE FROM kunden WHERE id = 1;" },
                    new Card { Front = "ORDER BY", Back = "SELECT * FROM kunden ORDER BY name ASC;" },
                    new Card { Front = "JOIN (INNER JOIN)", Back = "SELECT k.name, b.betrag  \r\nFROM kunden k  \r\nJOIN bestellungen b ON k.id = b.kunden_id;" }
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


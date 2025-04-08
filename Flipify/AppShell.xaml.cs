using Flipify.View;

namespace Flipify;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainView), typeof(MainView));
        Routing.RegisterRoute(nameof(DeckView), typeof(DeckView));
        Routing.RegisterRoute(nameof(AddDeckView), typeof(AddDeckView));

    }
}

using Flipify.Model;
using Flipify.Service;
using Flipify.View;
using Flipify.ViewModel;
using Microsoft.Extensions.Logging;

namespace Flipify
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Dependency Injection View
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddTransient<DeckView>();
            builder.Services.AddTransient<AddCardView>();
            builder.Services.AddTransient<AddDeckView>();

            //Dependency Injection ViewModel
            builder.Services.AddSingleton<BaseVM>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<DeckVM>();

            //Dependency Injection Model
            builder.Services.AddSingleton<Card>();
            builder.Services.AddSingleton<Deck>();

            //Dependency Injection Service
            builder.Services.AddSingleton<DeckService>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

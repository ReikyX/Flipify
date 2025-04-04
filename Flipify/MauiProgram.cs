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

            //Dependency Injection ViewModel
            builder.Services.AddSingleton<MainViewModel>();

            //Dependency Injection Model

            //....




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

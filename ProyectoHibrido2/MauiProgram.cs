using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ProyectoHibrido2.Services;
using ProyectoHibrido2.Validations;

namespace ProyectoHibrido2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("ROBOTO-LIGHT", "light");
                    fonts.AddFont("ROBOTO-MEDIUM", "medium");
                    fonts.AddFont("ROBOTO-REGULAR", "regular");
                    fonts.AddFont("Font Awesome 6 Duotone-Solid-900.otf", "AwesomeSolid");
                });
           
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<IValidator, Validator>();

            return builder.Build();
        }
    }
}

using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TestApp.Data.Repositories;
using TestApp.ViewModels;
using TestApp.Views;

namespace TestApp
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DemoModelRepository>(s =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "demo.db");
                Debug.WriteLine($"Database path: {dbPath}");
                return new DemoModelRepository(dbPath);
            });

            builder.Services.AddSingleton<DemoViewModel>();
            builder.Services.AddTransient<DemoView>(s => new DemoView
            {
                BindingContext = s.GetRequiredService<DemoViewModel>()
            });

            return builder.Build();
        }
    }
}
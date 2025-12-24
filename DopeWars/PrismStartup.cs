using DopeWars.Interfaces;
using DopeWars.Models;
using DopeWars.Views;

namespace DopeWars;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
            .CreateWindow("NavigationPage/MainPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>()
            .RegisterInstance(SemanticScreenReader.Default);
        containerRegistry.RegisterSingleton<IUserData, UserData>();
    }
}

using DopeWars.Interfaces;
using DopeWars.Models;
using DopeWars.ViewModels;
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

        containerRegistry.RegisterForNavigation<SelectedCityView, SelectedCityViewModel>();

        containerRegistry.RegisterSingleton<IUserData, UserData>();
    }
}

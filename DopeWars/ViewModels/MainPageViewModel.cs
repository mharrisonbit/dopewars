using System.Diagnostics;
using DopeWars.Models;
using DopeWars.Views;

namespace DopeWars.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    public DelegateCommand<City> SelectionCommand { get; }

    public MainPageViewModel(UserData currentUser,ISemanticScreenReader screenReader, INavigationService navigationService, IPageDialogService dialogService) : base(currentUser, screenReader, navigationService, dialogService)
    {
        SelectionCommand = new DelegateCommand<City>(async (x) => await SelectionMade(x));

        Title = "Home Page";
        Task.Run(async () => await ShowBusy(LoadCityAndDrugData, "Loading data..."));
    }

    private async Task SelectionMade(City city)
    {
        //Navigate to the selected city
        Debug.WriteLine($"Selection {city.Name}");
        await Task.Delay(500);

        var parameters = new NavigationParameters
        {
            { "SelectedCity", city },
        };

        var result = await NavigationService.NavigateAsync(nameof(SelectedCityView), parameters);
        if (!result.Success)
        {
            Debug.WriteLine(result.Exception);
        }
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        Debug.WriteLine(ListOfDrugs);
        Debug.WriteLine(ListOfCities);
    }
}

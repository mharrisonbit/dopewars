using System.Diagnostics;
using DopeWars.Models;

namespace DopeWars.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    public DelegateCommand<Cities> SelectionCommand { get; }

    public MainPageViewModel(MyData currentUser,ISemanticScreenReader screenReader, INavigationService navigationService, IPageDialogService dialogService) : base(currentUser, screenReader, navigationService, dialogService)
    {
        SelectionCommand = new DelegateCommand<Cities>(async (x) => await SelectionMade(x));

        Title = "Home Page";
        Task.Run(async () => await ShowBusy(LoadCityAndDrugData, "Loading data..."));
        CashAmount = 100;
    }


    private async Task<bool> LoadCityAndDrugData()
    {
        await Task.Delay(5000);
        await using var citiesStream = await FileSystem.OpenAppPackageFileAsync("Data/CitiesData.json");
        using var citiesReader = new StreamReader(citiesStream);
        var cityText = await citiesReader.ReadToEndAsync();
        ListOfCities= Cities.FromJson(cityText);

        await using var drugStream = await FileSystem.OpenAppPackageFileAsync("Data/DrugsData.json");
        using var drugReader = new StreamReader(drugStream);
        var drugText = await drugReader.ReadToEndAsync();
        ListOfDrugs = Drugs.FromJson(drugText);

        return true;
    }

    private async Task SelectionMade(Cities city)
    {
        //Navigate to the selected city
        Debug.WriteLine($"Selection {city.Name}");
        await Task.Delay(500);
    }
}

using System.Collections.ObjectModel;
using DopeWars.Interfaces;
using DopeWars.Models;
using Mopups.PreBaked.PopupPages.Loader;
using Mopups.PreBaked.Services;
using Newtonsoft.Json;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace DopeWars.ViewModels;

public class BaseViewModel : BindableBase, INavigationAware
{
    public IUserData CurrentUser { get; set; }
    protected INavigationService NavigationService { get; private set; }
    private ISemanticScreenReader ScreenReader { get; }
    public IPageDialogService DialogService { get; set; }

    public DelegateCommand<double?> DepositCash { get; }
    public DelegateCommand<double?> WithdrawCash { get; }

    protected BaseViewModel(IUserData currentUser,ISemanticScreenReader screenReader,INavigationService navigationService, IPageDialogService dialogService)
    {
        CurrentUser = currentUser;
        ScreenReader = screenReader;
        NavigationService = navigationService;
        DialogService = dialogService;

        DepositCash = new DelegateCommand<double?>(
            async (x) => await CurrentUser.MakeDeposit(x)).ObservesProperty(() => IsBusy);
        WithdrawCash = new DelegateCommand<double?>(
            async (x) => await CurrentUser.MakeWithdraw(x)).ObservesProperty(() => IsBusy);
    }

    public string Title
    {
        get;
        set => SetProperty(ref field, value);
    } = "Click me";

    public ObservableCollection<City> ListOfCities
    {
        get;
        set => SetProperty(ref field, value);
    }

    public ObservableCollection<Drug> ListOfDrugs
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsBusy
    {
        get;
        set => SetProperty(ref field, value);
    }

    protected async Task<T> ShowBusy<T>(Func<Task<T>>  methodToCall, string waitMessage)
    {
        return await PreBakedMopupService.GetInstance().WrapReturnableTaskInLoader<T, LoaderPopupPage>(methodToCall(),
            Colors.Blue, Colors.White, LoadingReasons(waitMessage), Colors.Black);
    }

    private List<string> LoadingReasons(string message = "Working")
    {
        List<string> testing = [message];
        return testing;

    }

    protected async Task<bool> LoadCityAndDrugData()
    {
        await Task.Delay(500); // optional splash delay

        await using var stream =
            await FileSystem.OpenAppPackageFileAsync("Data/GameSetupData.json");

        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        var gameData = JsonConvert.DeserializeObject<GameData>(json);


        var rand = new Random();

/* Observable collections for binding */
        ListOfCities = new ObservableCollection<City>(gameData.Cities);
        ListOfDrugs  = new ObservableCollection<Drug>(gameData.Drugs);

/* Randomize drug availability per city */
        foreach (var city in ListOfCities)
        {
            foreach (var drug in ListOfDrugs)
            {
                // 70% chance a drug exists in the city
                if (rand.NextDouble() < 0.7)
                {
                    var price =
                        drug.PriceRange[rand.Next(drug.PriceRange.Count)];

                    city.AvailableDrugs.Add(new CityDrug
                    {
                        Name = drug.Name,
                        Price = price
                    });
                }
            }
        }
        return true;
    }

    public virtual void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public virtual void OnNavigatedTo(INavigationParameters parameters)
    {
    }
}

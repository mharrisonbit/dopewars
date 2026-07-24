using System.Collections.ObjectModel;
using System.Diagnostics;
using DopeWars.Interfaces;
using DopeWars.Models;

namespace DopeWars.ViewModels;

public class SelectedCityViewModel : BaseViewModel
{
    public DelegateCommand<CityDrug> SelectionCommand { get; set; }
    private ObservableCollection<CityDrug> AllItems { get; set; }

    public SelectedCityViewModel(IUserData currentUser, ISemanticScreenReader screenReader, INavigationService navigationService, IPageDialogService dialogService) : base(currentUser, screenReader, navigationService, dialogService)
    {
        SelectionCommand = new DelegateCommand<CityDrug>(async (x) => await SelectionMade(x));
        Debug.WriteLine("this is the list of drugs " + ListOfDrugsToSell);
    }


    private async Task SelectionMade(CityDrug drugs)
    {
        Debug.WriteLine(drugs);
    }

    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        Title = parameters.GetValue<City>("SelectedCity").Name;
        AllItems = parameters.GetValue<City>("SelectedCity").AvailableDrugs;
        await UpdateListForDisplay();
    }


    private async Task UpdateListForDisplay()
    {
        var randomSubset = AllItems
            .OrderBy(_ => Random.Shared.Next())
            .Take(3)
            .ToList();

        ListOfDrugsToSell.Clear();
        foreach (var item in randomSubset)
        {
            ListOfDrugsToSell.Add(item);
        }

        randomSubset.Clear();

        //TODO: need to add a settings page and allow the user to change the number of drugs that can be shown in the lists.
        randomSubset = AllItems
            .OrderBy(_ => Random.Shared.Next())
            .Take(3)
            .ToList();

        ListOfDrugsToBuy.Clear();
        foreach (var item in randomSubset)
        {
            ListOfDrugsToBuy.Add(item);
        }
    }
}

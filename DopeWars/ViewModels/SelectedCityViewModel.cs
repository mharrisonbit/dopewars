using System.Diagnostics;
using DopeWars.Interfaces;
using DopeWars.Models;

namespace DopeWars.ViewModels;

public class SelectedCityViewModel : BaseViewModel
{
    public DelegateCommand<CityDrug> SelectionCommand { get; set; }

    public SelectedCityViewModel(IUserData currentUser, ISemanticScreenReader screenReader, INavigationService navigationService, IPageDialogService dialogService) : base(currentUser, screenReader, navigationService, dialogService)
    {
        SelectionCommand = new DelegateCommand<CityDrug>(async (x) => await SelectionMade(x));
        Debug.WriteLine("this is the list of drugs " + ListOfDrugs);
    }


    private async Task SelectionMade(CityDrug drugs)
    {
        Debug.WriteLine(drugs);
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        Title = parameters.GetValue<City>("SelectedCity").Name;
        ListOfDrugs = parameters.GetValue<City>("SelectedCity").AvailableDrugs;
    }
}

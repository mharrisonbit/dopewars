using System.Diagnostics;
using DopeWars.Interfaces;
using DopeWars.Models;

namespace DopeWars.ViewModels;

public class SelectedCityViewModel : BaseViewModel
{
    public DelegateCommand<Drug> SelectionCommand { get; set; }

    public SelectedCityViewModel(IUserData currentUser, ISemanticScreenReader screenReader, INavigationService navigationService, IPageDialogService dialogService) : base(currentUser, screenReader, navigationService, dialogService)
    {
        SelectionCommand = new DelegateCommand<Drug>(async (x) => await SelectionMade(x));
        Debug.WriteLine("this is the list of drugs " + ListOfDrugs);
    }


    private async Task SelectionMade(Drug drugs)
    {
        Debug.WriteLine(drugs);
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        Title = parameters.GetValue<City>("SelectedCity").Name;
        Debug.WriteLine(ListOfDrugs);
        Debug.WriteLine(ListOfCities);

    }
}

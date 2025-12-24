using System.Collections.ObjectModel;
using DopeWars.Interfaces;
using DopeWars.Models;
using Mopups.PreBaked.PopupPages.Loader;
using Mopups.PreBaked.Services;

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

        DepositCash = new DelegateCommand<double?>(async (x) => await CurrentUser.MakeDeposit(x)).ObservesProperty(() => IsBusy);
        WithdrawCash = new DelegateCommand<double?>(async (x) => await CurrentUser.MakeWithdraw(x)).ObservesProperty(() => IsBusy);
        CurrentUser.Cash = 1000;
    }


    public string Title
    {
        get;
        set => SetProperty(ref field, value);
    } = "Click me";

    public ObservableCollection<Cities> ListOfCities
    {
        get;
        set => SetProperty(ref field, value);
    }

    public Drugs ListOfDrugs
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
        var testing = new List<string>();
        testing.Add(message);
        return testing;

    }

    public virtual void OnNavigatedFrom(INavigationParameters parameters)
    {

    }

    public virtual void OnNavigatedTo(INavigationParameters parameters)
    {

    }
}

using System.Collections.ObjectModel;
using DopeWars.Models;
using Mopups.PreBaked.PopupPages.Loader;
using Mopups.PreBaked.Services;

namespace DopeWars.ViewModels;

public class BaseViewModel : BindableBase, INavigationAware
{
    public MyData CurrentUser { get; set; }
    protected INavigationService NavigationService { get; private set; }
    private ISemanticScreenReader ScreenReader { get; }
    public IPageDialogService DialogService { get; set; }

    public DelegateCommand<double?> DepositCash { get; }
    public DelegateCommand<double?> WithdrawCash { get; }

    public BaseViewModel(MyData currentUser,ISemanticScreenReader screenReader,INavigationService navigationService, IPageDialogService dialogService)
    {
        CurrentUser = currentUser;
        ScreenReader = screenReader;
        NavigationService = navigationService;
        DialogService = dialogService;

        DepositCash = new DelegateCommand<double?>(async (x) => await MakeDeposit(x)).ObservesProperty(() => IsBusy);
        WithdrawCash = new DelegateCommand<double?>(async (x) => await MakeWithdraw(x)).ObservesProperty(() => IsBusy);
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

    public double DebitAmount
    {
        get;
        set => SetProperty(ref field, value);
    }

    public double CashAmount
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsBusy
    {
        get;
        set => SetProperty(ref field, value);
    }

    public async Task MakeWithdraw(double? withdrawAmount)
    {

    }

    public async Task MakeDeposit(double? depositAmount)
    {

    }

    public async Task<T> ShowBusy<T>(Func<Task<T>>  methodToCall, string waitMessage)
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

    private void DebitPayment()
    {
        bool hasDebit = DebitAmount > 0;

        DebitAmount = hasDebit ? DebitAmount - 50 : 0;
        CashAmount  = hasDebit ? 0 : CashAmount + 50;
    }

    public virtual void OnNavigatedFrom(INavigationParameters parameters)
    {

    }

    public virtual void OnNavigatedTo(INavigationParameters parameters)
    {

    }
}

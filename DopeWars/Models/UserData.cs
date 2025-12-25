using System.Diagnostics;
using DopeWars.Interfaces;

namespace DopeWars.Models;

public class UserData : IUserData
{
    // [PrimaryKey, AutoIncrement]
    private int ID { get; set; }
    public string Name { get; set; }
    public double Cash{get; set;}
    public double Debit {get; set;}
    public List<Drugs> MyStash { get; set; }

    public async Task MakeDeposit(double? depositAmount)
    {
        Debug.WriteLine("Making deposit");
        await Task.Delay(5000);
        Cash = 1000;
    }

    public async Task MakeWithdraw(double? withdrawAmount)
    {
        Debug.WriteLine("Making withdraw");
        await Task.Delay(5000);
    }

    public void DebitPayment()
    {
        bool hasDebit = Debit > 0;

        Debit = hasDebit ? Debit - 50 : 0;
        Cash  = hasDebit ? 0 : Cash + 50;
    }
}

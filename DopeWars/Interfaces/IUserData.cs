using DopeWars.Models;

namespace DopeWars.Interfaces;

public interface IUserData
{
    // private int ID { get; set; }
    public string Name { get; set; }
    public double Cash{get; set;}
    public double Debit {get; set;}
    public List<Drugs> MyStash { get; set; }

    public Task MakeDeposit(double? depositAmount);
    public Task MakeWithdraw(double? withdrawAmount);
    public void DebitPayment();
}

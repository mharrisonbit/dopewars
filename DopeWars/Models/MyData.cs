namespace DopeWars.Models;

public class MyData
{
    // [PrimaryKey, AutoIncrement]
    private int ID { get; set; }
    public string Name { get; set; }
    public double Cash{get; set;}
    public double Debit {get; set;}
    public List<Drugs> MyStash { get; set; }

}

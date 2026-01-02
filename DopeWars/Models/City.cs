using System.Collections.ObjectModel;

namespace DopeWars.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ObservableCollection<CityDrug> AvailableDrugs { get; set; }
        = [];
}

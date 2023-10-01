using Assig1.Models;

namespace Assig1.ViewModels
{
    public class CountryDetailsViewModel
    {
        // Country detail properties
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? RegionName { get; set; }
        public int? RegionId { get; set; }

        //Aggregated data for emissions
        public decimal? AvgEmission { get; set; }
        public decimal? MaxEmission { get; set; }
        public decimal? MinEmission { get; set; }

        //Aggregate data f0r temperature
        public decimal? AvgTemperature { get; set; }
        public decimal? MaxTemperature { get; set; }
        public decimal? MinTemperature { get; set; }

        //List for details data
        public IEnumerable<TemperatureData>? TemperatureData { get; set; }
        public IEnumerable<CountryEmissionViewModel>? CountryEmissions { get; set; }

    }

    // for the item name with the emission data, 
    // separate ViewModel for CountryEmission that includes the item name.
    public class CountryEmissionViewModel
    {
        public int? Year { get; set; }
        public decimal? Value { get; set; }
        public string? ItemName { get; set; }
    }
}

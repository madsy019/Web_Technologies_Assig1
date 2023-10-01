using Assig1.Models;
using System.Collections.Generic;


namespace Assig1.ViewModels
{
    //City properties 
    public class CityDataViewModel
    {
        public string? CityName { get; set; } // Name of the city
        public int? EarliestYear { get; set; } // Earliest year for which air quality data is available for the city
        public int? LatestYear { get; set; } // Latest year for which air quality data is available for the city
        public int RecordCount { get; set; } // Total number of air quality data records available for the city
    }

    public class CitiesIndexViewModel
    {

    // Country and Region Information
    public string CountryName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? RegionName { get; set; }        

    }
}

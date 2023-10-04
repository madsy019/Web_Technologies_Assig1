namespace Assig1.ViewModels
{
    public class CityDetailsViewModel
    {
        //properties for the city details
        
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public string? RegionName { get; set; }
        public List<AirQualityDataInfo> AirQualityDataList { get; set; } = new List<AirQualityDataInfo>();
    }

    // Represents air quality data information for a specific year and item.
    public class AirQualityDataInfo
    {
        public int Year { get; set; }
        public string? ItemName { get; set; }
        public string? ElementName { get; set; }
        public string? Unit { get; set; }
        public int? AnnualMean { get; set; }
        public string? TemporalCoverage1 { get; set; }
        public string? AnnualMeanPm10 { get; set; }
        public int? AnnualMeanUgm3 { get; set; }
        public string? TemporalCoverage2 { get; set; }
        public string? AnnualMeanPm25 { get; set; }
        public List<MonitoringStationInfo> MonitoringStations { get; set; } = new List<MonitoringStationInfo>();
    }

    // Represents information about a monitoring station.
    public class MonitoringStationInfo
    {
        public string? StationType { get; set; }
        public int? Number { get; set; }
        public string? ImageUrl { get; set; }
    }

    // Represents a view model for city air quality information.
    public class CityAirQualityInfoViewModel
    {
        public int Year { get; set; }
        public string? ItemName { get; set; }
        public string? ElementName { get; set; }
        public int? AnnualMean { get; set; }
        public string? TemporalCoverage1 { get; set; }
        public string? AnnualMeanPm10 { get; set; }
        public int? AnnualMeanUgm3 { get; set; }
        public string? TemporalCoverage2 { get; set; }
        public string? AnnualMeanPm25 { get; set; }
        public string? Reference { get; set; }
        public int? DbYear { get; set; }
        public string? Status { get; set; }
    }
}

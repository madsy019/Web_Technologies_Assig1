using Assig1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}

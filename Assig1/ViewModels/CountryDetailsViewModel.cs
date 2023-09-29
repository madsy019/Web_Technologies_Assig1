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
    }
}

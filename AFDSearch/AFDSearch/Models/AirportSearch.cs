using System.Collections.Generic;

namespace AFDSearch.Models
{
    public class AirportSearch
    {
        public AirportSearch()
        {
            AirportSearchResults = new List<Airport>();
        }

        public string SearchText { get; set; }

        public string Filter { get; set; }

        public int Top { get; set; }

        public int Skip { get; set; }

        public long Count { get; set; }

        public List<Airport> AirportSearchResults { get; set; }

        public List<FacetInfo> FacetResults { get; set; }
    }
}
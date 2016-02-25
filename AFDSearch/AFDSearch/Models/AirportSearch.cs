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

        public List<Airport> AirportSearchResults { get; set; }
    }
}
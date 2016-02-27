using System.Collections.Generic;

namespace AFDSearch.Models
{
    public class FacetInfo
    {
        public FacetInfo()
        {
            Facets = new Dictionary<string, long>();
        }
        public string FacetName { get; set; }

        public Dictionary<string, long> Facets { get; set; }
    }
}
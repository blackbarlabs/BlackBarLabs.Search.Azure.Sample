using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AFDSearch.Models;
using BlackBarLabs.Search.Azure;

namespace AFDSearch.Controllers
{
    public class AirportController : Controller
    {
        private const int DefaultTopCount = 500;
        private readonly AzureSearchEngine azureSearchEngine;
        private string indexName;
        private string suggesterName;

        public AirportController()
        {
            indexName = ConfigurationManager.AppSettings["SearchServiceIndexName"];
            suggesterName = ConfigurationManager.AppSettings["SearchServiceSuggesterName"];
            var engines = new SearchEngines("SearchServiceName", "SearchServiceApiKey");
            azureSearchEngine = engines.AzureSearchEngine;
        }
        // GET: Search
        public async Task<ActionResult> Search(AirportSearch model)
        {
            // By default, we want to get back no results, but we want to get facet and count information for the entire result set
            int? top = DefaultTopCount;
            var searchText = model.SearchText;
            if (string.IsNullOrEmpty(searchText) && string.IsNullOrEmpty(model.Filter))
            {
                searchText = "*";
                top = 0;
            }

            // Set up facets
            var facetfields = new List<string>() {"Region", "State", "Chart"};
            var facetResults = new List<FacetInfo>();

            var result = await azureSearchEngine.SearchDocumentsAsync<Airport>(indexName, searchText, facetfields, true, top, null,
                model.Filter, 
                airport => new Airport() {Id = airport.Id, Identifier = airport.Identifier, Name = airport.Name, City = airport.City, State = airport.State, Chart = airport.Chart, Region = airport.Region, AfdLink = airport.AfdLink},
                (facetName, facets) =>
                {
                    facetResults.Add(new FacetInfo() {FacetName = facetName, Facets = facets});
                }, 
                count => { });

            var resultsModel = new AirportSearch();
            resultsModel.SearchText = model.SearchText;
            resultsModel.AirportSearchResults = result.ToList();
            resultsModel.FacetResults = facetResults;
            resultsModel.Filter = model.Filter;

            return View(resultsModel);
        }
    }
}
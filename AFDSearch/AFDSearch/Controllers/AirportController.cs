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

            var result = await azureSearchEngine.SearchDocumentsAsync<Airport>(indexName, model.SearchText, null, true, null, null,
                null, 
                airport => new Airport() {Id = airport.Id, Identifier = airport.Identifier, Name = airport.Name, City = airport.City, State = airport.State, Chart = airport.Chart, Region = airport.Region, AfdLink = airport.AfdLink},
                (s, longs) => { }, l => { });

            var resultsModel = new AirportSearch();
            resultsModel.SearchText = model.SearchText;
            resultsModel.AirportSearchResults = result.ToList();

            return View(resultsModel);
        }
    }
}
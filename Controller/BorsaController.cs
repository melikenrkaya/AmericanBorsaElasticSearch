using AmericanBorsaElasticSearch.Services.ElasticSearchServi;
using AmericanBorsaElasticSearch.Services.ScraperServi;
using Elasticsearch.Net;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AmericanBorsaElasticSearch.Controller
{
    [ApiController]
    [Route("api/hisse")]
    public class BorsaController : ControllerBase
    {
        private readonly ElasticServi _elasticService;
        private readonly BorsaServi _borsaServi;

        public BorsaController(ElasticServi elasticService, BorsaServi borsaServi)
        {
            _elasticService = elasticService;
            _borsaServi = borsaServi;
        }

        [HttpPost("scrape")]
        public async Task<IActionResult> ScrapeAndIndex()
        {
            await _borsaServi.AmericanBorsaVeriCekme();
            return Ok("Hisse senetleri başarıyla Elasticsearch'e eklendi.");
        }



        [HttpGet("cleanresponse")]
        public async Task<IActionResult> GetStockData(string query)
        {
            var response = await _elasticService.SearchHisse(query);
            Console.WriteLine("ElasticSearch Response: " + response);  // Dönen cevabı yazdırıyoruz

            var searchResult = JsonConvert.DeserializeObject<ElasticSearchResponse>(response);
            // Yalnızca "Name" verileri
            var stockNames = searchResult.Hits.Hits.Select(hit => hit.Source.Name).ToList();
            return Ok(stockNames);
        }



        [HttpGet("search/{query}")]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _elasticService.SearchHisse(query);
            return Ok(results);
        }

    }
}

// Elasticsearch response model
public class ElasticSearchResponse
{
    [JsonProperty("hits")]
    public HitsInfo Hits { get; set; }
}

public class HitsInfo
{
    [JsonProperty("hits")]
    public List<Hit> Hits { get; set; }
}

public class Hit
{
    [JsonProperty("_source")]
    public Source Source { get; set; }
}

public class Source
{
    [JsonProperty("Name")]
    public string Name { get; set; }
}

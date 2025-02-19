using AmericanBorsaElasticSearch.Data.Entity;
using Elasticsearch.Net;
using System.Drawing;
using System.Text.Json;

namespace AmericanBorsaElasticSearch.Services.ElasticSearchServi
{
    public class ElasticServi
    {
        private readonly ElasticLowLevelClient _client;
        private const string IndexName = "my_index6"; // Elasticsearch index adı

        public ElasticServi()
        {
            var settings = new ConnectionConfiguration(new Uri("http://localhost:9200")) // Localhost URL'si
              .RequestTimeout(TimeSpan.FromMinutes(2)); /// Kullanıcı adı ve şifre

            _client = new ElasticLowLevelClient(settings);

        }

        public async Task<string> SearchHisse(string searchTerm)
        {
           
            var query = new
            {
                query = new
                {
                    match = new
                    {
                        Name = new
                        {
                            query = searchTerm,
                            fuzziness = "AUTO"  // Bulanık arama için "AUTO"
                        }
                    }
                },
      
            };
           
            var json = JsonSerializer.Serialize(query);
            var response = await _client.SearchAsync<StringResponse>(IndexName, PostData.String(json));

            if (response.Success)
            {
                Console.WriteLine(response.DebugInformation);
                return response.Body;
            }
            else
            {
                Console.WriteLine(response.DebugInformation);
                return "Arama sırasında hata oluştu.";
            }
        }

        //public async Task CreateIndexIfNotExist()
        //{
        //    var response = await _client.Indices.ExistsAsync(IndexName);

        //    if (!response.Success)
        //    {
        //        // Index yoksa oluşturuyoruz
        //        var createResponse = await _client.Indices.CreateAsync(IndexName, c => c
        //            .Map(m => m
        //                .AutoMap()
        //            )
        //        );

        //        if (!createResponse.Success)
        //        {
        //            Console.WriteLine("Index oluşturulamadı: " + createResponse.DebugInformation);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Index başarıyla oluşturuldu.");
        //        }
        //    }
        



        public async Task IndexHisseSenetleri(List<HisseSenedi> hisseListesi)
        {
            foreach (var hisse in hisseListesi)
            {
                var indexAction = new
                {
                    index = new
                    {
                        _index = "hisse_senetleri",
                        _id = Guid.NewGuid().ToString() // Her kaydın benzersiz bir ID'ye sahip olmasını sağlıyoruz
                    }
                };
                var json = JsonSerializer.Serialize(new { Name = hisse.Name });
                var response = await _client.IndexAsync<StringResponse>(IndexName, hisse.Name,  PostData.String(json));

                if (!response.Success)
                {
                    Console.WriteLine("Hata: " + response.DebugInformation);
                }
            }
        }

    }

}

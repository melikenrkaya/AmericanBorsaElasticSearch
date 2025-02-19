using Alpaca.Markets;
using AmericanBorsaElasticSearch.Data.Context;
using AmericanBorsaElasticSearch.Data.Entity;
using AmericanBorsaElasticSearch.Services.ElasticSearchServi;


namespace AmericanBorsaElasticSearch.Services.ScraperServi
{
    public class BorsaServi
    {
        private readonly ElasticServi _elasticService;
        private readonly ApplicationDBContext _context;

        // Alpaca API Key ve Secret
        private const string API_KEY = "PK6887VNETZQ3W3JOBAK";
        private const string SECRET_KEY = "k4mxtKHoHKI8O7QvGzBjOtWpe8b8rVnVZZgoumxM";
        public BorsaServi(ElasticServi elasticService, ApplicationDBContext context)
        {
            _elasticService = elasticService;
            _context = context;
        }

        public async Task AmericanBorsaVeriCekme()
        {
            try
            {
                var client = Alpaca.Markets.Environments.Paper.GetAlpacaTradingClient(new SecretKey(API_KEY, SECRET_KEY));

                // Tüm hisse senetlerini çek
                var assets = await client.ListAssetsAsync(new AssetsRequest());

                List<HisseSenedi> hisseListesi = new List<HisseSenedi>();

                foreach (var asset in assets)
                {
                    if (asset.Status == AssetStatus.Active && asset.Exchange == Exchange.Nasdaq)
                    {
                        var hisse = new HisseSenedi { Name = asset.Name };

                        hisseListesi.Add(hisse);
                        _context.HisseSenedii.Add(hisse);
                    }
                }

                await _context.SaveChangesAsync(); // Veritabanına kaydet
                await _elasticService.IndexHisseSenetleri(hisseListesi); // Elasticsearch’e ekle

                Console.WriteLine("Hisse senetleri başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }
    }
}


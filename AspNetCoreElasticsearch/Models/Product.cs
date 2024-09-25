using System.Text.Json.Serialization;

namespace AspNetCoreElasticsearch.Models
{
    public class Product:BaseProduct
    {

        [JsonPropertyName("_id")]
        public string? Id { get; set; }
    }
    public class BaseProduct
    {
        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; } 
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("stock")]
        public int? Stock { get; set; }
    }
}

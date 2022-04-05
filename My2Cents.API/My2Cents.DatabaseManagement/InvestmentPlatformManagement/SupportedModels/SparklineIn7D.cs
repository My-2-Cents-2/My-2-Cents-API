using Newtonsoft.Json;

namespace My2Cents.DatabaseManagement.Models
{
    public class SparklineIn7D
    {
        [JsonProperty("price")]
        public decimal[] Price { get; set; }
    }
}
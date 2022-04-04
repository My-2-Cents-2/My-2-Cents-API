using Newtonsoft.Json;

namespace My2Cents.DatabaseManagement.Models
{
    public class MarketDataStock
    {
        [JsonProperty("data")]
        public List<MarketDataStockDetail> Datas { get; set; }
    }

    public class MarketDataStockDetail
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("attributes")]
        public Attribute Attributes { get; set; }
    }

    public class Attribute
    {
        [JsonProperty("name")]
        public string LongName { get; set; }

        [JsonProperty("change")]
        public decimal RegMarketChange { get; set; }

        [JsonProperty("percentChange")]
        public double RegMarketChangePecent { get; set; }

        [JsonProperty("last")]
        public decimal RegMarketPrice { get; set; }

        [JsonProperty("dateTime")]
        public DateTime _dateTime { get; set; }
    }
}
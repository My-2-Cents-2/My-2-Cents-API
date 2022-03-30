using Newtonsoft.Json;

namespace My2Cents.DatabaseManagement.Models
{
    public class MarketDataOhlcv
    {
        [JsonProperty("market_cap_rank")]
        public long? MarketCapRank { get; set; }

        [JsonProperty("price_change_24h")]
        public decimal PriceChange24H { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public double PriceChangePercentage24H { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public decimal? MarketCapChange24H { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public decimal? MarketCapChangePercentage24H { get; set; }

        [JsonProperty("circulating_supply")]
        public string CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public decimal? TotalSupply { get; set; }
        
    }

    public class Roi
    {
        [JsonProperty("times")] public decimal? Times { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("percentage")] public decimal? Percentage { get; set; }
    }
}
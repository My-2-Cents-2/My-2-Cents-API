namespace My2Cents.DataInfrastructure.Models
{
    public class CryptoDto
    {
        public int CryptoId { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime LastUpdate { get; set; }
        public string ImageURL { get; set; }
        public decimal PriceChange { get; set; }
        public double PriceChangePercentage { get; set; }
        public string Name { get; set; } = null!;
        public string ShortenedName { get; set; } = null!;
        public string CryptoNameId { get; set; }
    }
}
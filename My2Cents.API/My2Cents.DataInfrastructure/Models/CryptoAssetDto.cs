namespace My2Cents.DataInfrastructure.Models
{
    public class CryptoAssetDto
    {
        public int CryptoAssetId { get; set; }
        public int CryptoId { get; set; }
        public int UserId { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal Quantity { get; set; }
        public int BuyCount { get; set; }
    }
}
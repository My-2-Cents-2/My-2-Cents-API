namespace My2Cents.DataInfrastructure.Models
{
    public class CryptoOrderHistoryDto
    {
        public int CryptoOrderId { get; set; }
        public int UserId { get; set; }
        public int CryptoId { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal Quantity { get; set; }
        public string OrderType { get; set; } = null!;
        public DateTime OrderTime { get; set; }
    }
}
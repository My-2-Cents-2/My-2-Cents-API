namespace My2Cents.DataInfrastructure.Models
{
    public class StockOrderHistoryDto
    {
        public int StockOrderId { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal Quantity { get; set; }
        public string OrderType { get; set; } = null!;
        public DateTime OrderTime { get; set; }
    }
}
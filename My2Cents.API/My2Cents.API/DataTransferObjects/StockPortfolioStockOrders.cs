namespace My2Cents.API.DataTransferObjects
{
    public class StockPortfolioStockOrderForm
    {
        public string UserName { get; set; }
        public string StockName { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal Quantity { get; set; }
        public string? OrderType { get; set; } = null!;
    } 
}  
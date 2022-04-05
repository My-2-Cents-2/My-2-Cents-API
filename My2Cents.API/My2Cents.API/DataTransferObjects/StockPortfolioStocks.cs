namespace My2Cents.API.DataTransferObjects
{
    public class StockPortfolioStockForm
    {
        public decimal CurrentPrice { get; set; }
        public string? Name { get; set; }
        public string? ShortenedName { get; set; }
    }
}
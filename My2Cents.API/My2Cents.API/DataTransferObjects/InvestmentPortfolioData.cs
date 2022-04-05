namespace My2Cents.API.DataTransferObjects
{
    public class StockPortfolioStockInvestmentForm
    {
        public string? Name { get; set; }
        public string? InitialInvestmentDate { get; set; }
        public decimal CurrentInvestment { get; set; }
        public decimal OwnedShares { get; set; }
        public decimal SharePrice { get; set; }
        public decimal Returns { get; set; }
        public decimal StockPrice { get; set; }
    }
}
namespace My2Cents.API.DataTransferObjects
{
    public class StockPortfolioStockInvestmentForm
    {
        public int Name { get; set; }
        public DateTime InitialInvestmentDate { get; set; }
        public int CurrentInvestment { get; set; }
        public decimal OwnedShares { get; set; }
        public decimal SharePrice { get; set; }
        public decimal Returns { get; set; }
        public decimal CryptoPrice { get; set; }
    }
}
namespace My2Cents.API.DataTransferObjects
{
    public class OrderHistoryPortfolioForm
    {
        public string Name { get; set; }
        public decimal CurrentInvestment { get; set; }
        public DateTime InitialInvestmentDate { get; set; }
        public decimal OwnedShares { get; set; }
        public string TransactionType { get; set; }
    }
}
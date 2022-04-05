namespace My2Cents.API.DataTransferObjects
{
    public class CryptoOrderHisT
    {
        public string Name { get; set; }
        public decimal CurrentInvestment { get; set; }
        public string InitialInvestmentDate { get; set; }
        public decimal OwnedShares { get; set; }
        public string TransactionType { get; set; }
    }
}
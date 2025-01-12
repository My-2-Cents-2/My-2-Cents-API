namespace My2Cents.API.DataTransferObjects
{
    public class CryptoAssetTable
    {
         public string Name { get; set; }
        public string InitialInvestmentDate { get; set; }
        public decimal CurrentInvestment { get; set; }
        public decimal OwnedShares { get; set; }
        public decimal SharePrice { get; set; }
        public decimal Returns { get; set; }
        public decimal CryptoPrice { get; set; }
    }
}
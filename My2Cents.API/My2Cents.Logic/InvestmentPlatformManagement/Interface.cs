using My2Cents.DataInfrastructure;


namespace PlatformBL
{
    public interface IPlatformBL
    {
        //Need to discuss some functions with Vijhan's team

        public CryptoOrderHistory PlaceOrderCrypto(CryptoAsset _asset, CryptoOrderHistory _cOrderHis, int _userID, int _cryptoID,  decimal _cryptoPrice, decimal amount);

        public CryptoOrderHistory SellOrderCrypto(CryptoAsset _asset, CryptoOrderHistory _cOrderHis, int _userID, int _cryptoID, decimal _cryptoPrice, decimal amount);

        public StockOrderHistory PlaceOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, int _userID, int _stockID, decimal _stockPrice, decimal amount);

        public StockOrderHistory SellOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, int _userID, int _stockID, decimal _stockPrice, decimal amount);


    }
}
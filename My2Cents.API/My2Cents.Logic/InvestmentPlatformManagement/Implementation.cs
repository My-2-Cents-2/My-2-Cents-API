using My2Cents.DataInfrastructure;
using My2Cents.DatabaseManagement;

namespace PlatformBL
{
    public class PlatformBL : IPlatformBL
    {
        private IRepository2 _repo;
        public PlatformBL(IRepository2 p_repo)
        {
            _repo = p_repo;
        }

        public CryptoOrderHistory PlaceOrderCrypto(CryptoAsset _asset, CryptoOrderHistory _cOrderHis, int _userID, int _cryptoID, decimal _cryptoPrice, decimal amount)
        {
            throw new NotImplementedException();
        }

        public StockOrderHistory PlaceOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, int _userID, int _stockID, decimal _stockPrice, decimal amount)
        {
            throw new NotImplementedException();
        }

        public CryptoOrderHistory SellOrderCrypto(CryptoAsset _asset, CryptoOrderHistory _cOrderHis, int _userID, int _cryptoID, decimal _cryptoPrice, decimal amount)
        {
            throw new NotImplementedException();
        }

        public StockOrderHistory SellOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, int _userID, int _stockID, decimal _stockPrice, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
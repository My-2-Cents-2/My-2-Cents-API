using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.Logic
{
    public interface IInvesmenentPlatformManagementBL
    {
        //BUYING MANAGEMENT
        Task<CryptoOrderHistoryDto> PlaceOrderCrypto(int _userID, int _cryptoID, decimal amount);
        Task<StockOrderHistoryDto> PlaceOrderStock(int p_userID, int p_stockID, decimal amount);
        Task<CryptoOrderHistoryDto> PlaceOrderCryptoFiat(int p_userID, int p_cryptoID, decimal amount);
        Task<StockOrderHistoryDto> PlaceOrderStockFiat(int p_userID, int p_stockID, decimal amount);


        //SELLING MANAGEMENT
        Task<CryptoOrderHistoryDto> SellCrypto(int p_userID, int p_cryptoID, decimal amount);
        Task<StockOrderHistoryDto> SellStock(int p_userID, int p_stockID, decimal amount);

        Task<CryptoOrderHistoryDto> SellCryptoFiat(int p_userID, int p_cryptoID, decimal amount);
        Task<StockOrderHistoryDto> SellStockFiat(int p_userID, int p_stockID, decimal amount);

        //CRYPTO MANAGEMENT
        Task<List<CryptoDto>> UpdateCryptosData();

        //STOCK MANAGEMENT
        Task<List<StockDto>> UpdateStocksData();


        // public CryptoOrderHistory SellOrderCrypto(CryptoAsset _asset, CryptoOrderHistory _cOrderHis, Account _balance, int _userID, int _cryptoID, decimal _cryptoPrice, decimal amount);

        // public StockOrderHistory PlaceOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, Account _balance, int _userID, int _stockID, decimal _stockPrice, decimal amount);

        // public StockOrderHistory SellOrderStock(StockAsset _asset, StockOrderHistory _sOrderHis, Account _balance, int _userID, int _stockID, decimal _stockPrice, decimal amount);


    }
}
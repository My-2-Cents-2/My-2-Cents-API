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
    }
}

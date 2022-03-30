using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.DatabaseManagement
{
    public interface IInvesmenentPlatformManagementDL
    {
        // CryptoAsset BuyCrypto(CryptoAsset _asset);
        // CryptoAsset BuyExistingCrypto(CryptoAsset _asset);
        // Account AddtoAccount(Account _balance);
        // Account SubtractFromAccount(Account _balance);
        // CryptoAsset SellCrypto(CryptoAsset _asset);
        // StockAsset BuyStock(StockAsset _asset);
        // StockAsset BuyExistingStock(StockAsset _asset);
        // StockAsset SellStock(StockAsset _asset);

        //BUYING MANAGEMENT
        CryptoOrderHistoryDto PlaceOrderCrypto(int _userID, int _cryptoID, decimal amount);
        StockOrderHistoryDto PlaceOrderStock(int p_userID, int p_stockID, decimal amount);
        CryptoOrderHistoryDto PlaceOrderCryptoFiat(int p_userID, int p_cryptoID, decimal amount);
        StockOrderHistoryDto PlaceOrderStockFiat(int p_userID, int p_stockID, decimal amount);

        //SELLING MANAGEMENT
        CryptoOrderHistoryDto SellCrypto(int _userID, int _cryptoID, decimal amount);
        StockOrderHistoryDto SellStock(int p_userID, int p_stockID, decimal amount);
        CryptoOrderHistoryDto SellCryptoFiat(int p_userID, int p_cryptoID, decimal amount);
        StockOrderHistoryDto SellStockFiat(int p_userID, int p_stockID, decimal amount);

        //CRYPTO MANAGEMENT
        Task<List<CryptoDto>> UpdateCryptosData();
    }
}


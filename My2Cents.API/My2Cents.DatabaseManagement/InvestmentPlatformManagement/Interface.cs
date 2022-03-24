using My2Cents.DataInfrastructure;
using PlatformDL;

public interface IRepository2
{
    CryptoAsset BuyCrypto(CryptoAsset _asset);
    CryptoAsset BuyExistingCrypto(CryptoAsset _asset);
    Account AddtoAccount(Account _balance);
    Account SubtractFromAccount(Account _balance);
    CryptoAsset SellCrypto(CryptoAsset _asset);
    StockAsset BuyStock(StockAsset _asset);
    StockAsset BuyExistingStock(StockAsset _asset);
    StockAsset SellStock(StockAsset _asset);

}

using My2Cents.DataInfrastructure;

namespace My2Cents.DatabaseManagement.Interfaces
{
    public interface ICryptoPortfolioDL
    {
        Crypto AddCrypto(Crypto _crypto);

        List<Crypto> GetAllCrypto();

        Crypto UpdateCryptoPrice(int _ID, decimal _price);

        CryptoOrderHistory AddCryptoOrderHistory(CryptoOrderHistory _cOrderHis);

        List<CryptoOrderHistory> GetCryptoOrderHisByUser(int _ID);
        

    }
}
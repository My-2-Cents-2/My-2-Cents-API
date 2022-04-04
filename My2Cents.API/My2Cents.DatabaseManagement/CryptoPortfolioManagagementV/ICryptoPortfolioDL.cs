using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.DatabaseManagement.Interfaces
{
    public interface ICryptoPortfolioDL
    {

        List<CryptoDto> GetAllCrypto();
        CryptoDto GetCryptoById(int _cryptoId);

        CryptoOrderHistoryDto AddCryptoOrderHistory(CryptoOrderHistory _cOrderHis);

        List<CryptoOrderHistoryDto> GetCryptoOrderHisByUser(int _ID);

        List<CryptoAssetDto> GetAllCryptoAssets();

        List<CryptoAssetDto> GetCryptoAssetsByUser(int _userID);

    }
}
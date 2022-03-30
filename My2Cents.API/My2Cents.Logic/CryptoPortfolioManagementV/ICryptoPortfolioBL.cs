using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.Logic.Interfaces
{
    public interface ICryptoPortfolioBL
    {
        CryptoDto AddCrypto(Crypto _crypto);

        List<CryptoDto> GetAllCrypto();

        CryptoDto GetCryptoById(int _cryptoId);

        CryptoDto UpdateCryptoPrice(int _ID, decimal _price);

        CryptoOrderHistoryDto AddCryptoOrderHistory(CryptoOrderHistory _cOrderHis);

        List<CryptoOrderHistoryDto> GetCryptoOrderHisByUser(int _ID);

        List<CryptoAssetDto> GetAllCryptoAssets();

        List<CryptoAssetDto> GetCryptoAssetsByUser(int _userID);

    }
}
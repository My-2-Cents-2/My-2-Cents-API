using My2Cents.DataInfrastructure;
using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.Logic.Interfaces
{
    public class CryptoPortfolioBL : ICryptoPortfolioBL
    {
        private readonly ICryptoPortfolioDL _repo;

        public CryptoPortfolioBL(ICryptoPortfolioDL repo)
        {
            _repo = repo;
        }

        public CryptoDto AddCrypto(Crypto _crypto)
        {
            return _repo.AddCrypto(_crypto);
        }

        public CryptoOrderHistoryDto AddCryptoOrderHistory(CryptoOrderHistory _cOrderHis)
        {
            return _repo.AddCryptoOrderHistory(_cOrderHis);
        }

        public List<CryptoDto> GetAllCrypto()
        {
            return _repo.GetAllCrypto();
        }

        public List<CryptoAssetDto> GetAllCryptoAssets()
        {
            return _repo.GetAllCryptoAssets();
        }

        public List<CryptoAssetDto> GetCryptoAssetsByUser(int _userID)
        {
            return _repo.GetCryptoAssetsByUser(_userID);
        }

        public CryptoDto GetCryptoById(int _cryptoId)
        {
            return _repo.GetCryptoById(_cryptoId);
        }

        public List<CryptoOrderHistoryDto> GetCryptoOrderHisByUser(int _ID)
        {
            return _repo.GetCryptoOrderHisByUser(_ID);
        }

        public CryptoDto UpdateCryptoPrice(int _ID, decimal _price)
        {
            return _repo.UpdateCryptoPrice(_ID, _price);
        }
    }
}
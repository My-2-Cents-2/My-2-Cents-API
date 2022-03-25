using My2Cents.DataInfrastructure;
using My2Cents.DatabaseManagement.Interfaces;

namespace My2Cents.Logic.Interfaces
{
    public class CryptoPortfolioBL : ICryptoPortfolioBL
    {
        private readonly ICryptoPortfolioDL _repo;

        public CryptoPortfolioBL(ICryptoPortfolioDL repo)
        {
            _repo = repo;
        }

        public Crypto AddCrypto(Crypto _crypto)
        {
            return _repo.AddCrypto(_crypto);
        }

        public CryptoOrderHistory AddCryptoOrderHistory(CryptoOrderHistory _cOrderHis)
        {
            return _repo.AddCryptoOrderHistory(_cOrderHis);
        }

        public List<Crypto> GetAllCrypto()
        {
            return _repo.GetAllCrypto();
        }

        public List<CryptoOrderHistory> GetCryptoOrderHisByUser(int _ID)
        {
            return _repo.GetCryptoOrderHisByUser(_ID);
        }

        public Crypto UpdateCryptoPrice(int _ID, decimal _price)
        {
            return _repo.UpdateCryptoPrice(_ID, _price);
        }

        public List<CryptoAsset> GetAllCryptoAssets()
        {
            try
            {
                return _repo.GetAllCryptoAssets();
            }
            catch(System.Exception exe )
            {
                throw new Exception(exe.Message);
            }
        }
        public List<CryptoAsset> GetUserCryptoAssets(int userId)
        {
            try
            {
                return _repo.GetUserCryptoAssets(userId);
            }
            catch(System.Exception exe )
            {
                throw new Exception(exe.Message);
            }
        } 
    }
}
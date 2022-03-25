using My2Cents.DataInfrastructure;
using Microsoft.EntityFrameworkCore;


namespace My2Cents.DatabaseManagement.Interfaces
{
    public class CryptoPortfolioDL : ICryptoPortfolioDL
    {
        private readonly My2CentsContext _context;

        public CryptoPortfolioDL(My2CentsContext context)
        {
            _context = context;
        }
        public Crypto AddCrypto(Crypto _crypto)
        {
        
           _context.Cryptos.Add(_crypto);
           _context.SaveChanges();
            return _crypto;
        }

        public CryptoOrderHistory AddCryptoOrderHistory(CryptoOrderHistory _cOrderHis)
        {
            _context.CryptoOrderHistories.Add(_cOrderHis);
            _context.SaveChanges();
            return _cOrderHis;
        }

        public List<Crypto> GetAllCrypto()
        {
            return _context.Cryptos.ToList();
        }

        public List<CryptoAsset> GetAllCryptoAssets()
        {
            return _context.CryptoAssets.ToList();
        }

        public List<CryptoAsset> GetCryptoAssetsByUser(int _userID)
        {
            return _context.CryptoAssets.Where(g => g.UserId == _userID).ToList();
        }

        public List<CryptoOrderHistory> GetCryptoOrderHisByUser(int _ID)
        {
            return _context.CryptoOrderHistories.Where(g => g.UserId == _ID).ToList();
        }

        public Crypto UpdateCryptoPrice(int _ID, decimal _price)
        {
            Crypto _ucrypto = _context.Cryptos.Where(g => g.CryptoId == _ID).FirstOrDefault();
            if (_ucrypto != null)
            {
                _ucrypto.CurrentPrice = _price;
                _ucrypto.LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            }else
            {
                throw new Exception("Crypto not found, cannot update info");
            }
            _context.SaveChanges();
            return _ucrypto;
        }
    }
}
using My2Cents.DataInfrastructure;

namespace PlatformDL
{
    public class DbContextRepository : IRepository2
    {
         private readonly My2CentsContext _context;

        public DbContextRepository(My2CentsContext context)
        {
            _context = context;
        }

        public Account AddtoAccount(Account _balance)
        {
            _context.Accounts.Add(_balance);
            _context.SaveChanges();

            return _balance;
        }

        public CryptoAsset BuyCrypto(CryptoAsset _asset)
        {
            _context.CryptoAssets.Add(_asset);
            _context.SaveChanges();

            return _asset;
        }

        public CryptoAsset BuyExistingCrypto(CryptoAsset _asset)
        {
            _context.CryptoAssets.Update(_asset);
            _context.SaveChanges();

            return _asset;
        }

        public StockAsset BuyExistingStock(StockAsset _asset)
        {
            _context.StockAssets.Update(_asset);
            _context.SaveChanges();

            return _asset;
        }

        public StockAsset BuyStock(StockAsset _asset)
        {
            _context.StockAssets.Add(_asset);
            _context.SaveChanges();

            return _asset;
        }

        public CryptoAsset SellCrypto(CryptoAsset _asset)
        {
            _context.CryptoAssets.Update(_asset);
            _context.SaveChanges();

            return _asset;
        }

        public StockAsset SellStock(StockAsset _asset)
        {
            _context.StockAssets.Update(_asset);
            _context.SaveChanges();

            return _asset;
        }

        public Account SubtractFromAccount(Account _balance)
        {
            _context.Accounts.Update(_balance);
            _context.SaveChanges();

            return _balance;
        }
    }
}
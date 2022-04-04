using My2Cents.DataInfrastructure;
using Microsoft.EntityFrameworkCore;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.DatabaseManagement.Interfaces
{
    public class CryptoPortfolioDL : ICryptoPortfolioDL
    {
        private readonly My2CentsContext _context;

        public CryptoPortfolioDL(My2CentsContext context)
        {
            _context = context;
        }
        public CryptoDto AddCrypto(Crypto _crypto)
        {
        CryptoDto _newc = new CryptoDto()
        {
            CryptoId = _crypto.CryptoId,
            CurrentPrice = _crypto.CurrentPrice,
            LastUpdate = _crypto.LastUpdate,
            Name = _crypto.Name,
            ShortenedName = _crypto.ShortenedName,
            
        };

           _context.Cryptos.Add(_crypto);
           _context.SaveChanges();
            return _newc;
        }

        public CryptoOrderHistoryDto AddCryptoOrderHistory(CryptoOrderHistory _cOrderHis)
        {
            CryptoOrderHistoryDto _orderhis = new CryptoOrderHistoryDto()
            {
                CryptoOrderId = _cOrderHis.CryptoOrderId,
                UserId = _cOrderHis.UserId,
                CryptoId = _cOrderHis.CryptoId,
                OrderPrice = _cOrderHis.OrderPrice,
                Quantity = _cOrderHis.Quantity,
                OrderTime = _cOrderHis.OrderTime,
                OrderType = _cOrderHis.OrderType
            };
            _context.CryptoOrderHistories.Add(_cOrderHis);
            _context.SaveChanges();
            return _orderhis;
        }

        public List<CryptoDto> GetAllCrypto()
        {
            return _context.Cryptos
            .Select(p => new CryptoDto
            {
                CryptoId = p.CryptoId,
                CurrentPrice = p.CurrentPrice,
                LastUpdate = p.LastUpdate,
                Name = p.Name,
                ShortenedName = p.ShortenedName,
                ImageURL = p.ImageURL
            }).ToList();
        }

        public List<CryptoAssetDto> GetAllCryptoAssets()
        {
            return _context.CryptoAssets
            .Select(p => new CryptoAssetDto
            {
                CryptoAssetId = p.CryptoAssetId,
                CryptoId = p.CryptoId,
                UserId = p.UserId,
                BuyPrice = p.BuyPrice,
                BuyDate = p.BuyDate,
                StopLoss = p.StopLoss,
                TakeProfit = p.TakeProfit,
                Quantity = p.Quantity
            }).ToList();
        }

        public List<CryptoAssetDto> GetCryptoAssetsByUser(int _userID)
        {
            return _context.CryptoAssets.Where(g => g.UserId == _userID)
            .Select(p => new CryptoAssetDto
            {
                CryptoAssetId = p.CryptoAssetId,
                CryptoId = p.CryptoId,
                UserId = p.UserId,
                BuyPrice = p.BuyPrice,
                BuyDate = p.BuyDate,
                StopLoss = p.StopLoss,
                TakeProfit = p.TakeProfit,
                Quantity = p.Quantity
            }).ToList();
        }

        public CryptoDto GetCryptoById(int _cryptoId)
        {
            return _context.Cryptos.Where(g => g.CryptoId == _cryptoId)
            .Select(p => new CryptoDto
            {
                CryptoId = p.CryptoId,
                CurrentPrice = p.CurrentPrice,
                LastUpdate = p.LastUpdate,
                Name = p.Name,
                ShortenedName = p.ShortenedName,
                ImageURL = p.ImageURL
            }).FirstOrDefault();
        }

        public List<CryptoOrderHistoryDto> GetCryptoOrderHisByUser(int _ID)
        {
            return _context.CryptoOrderHistories.Where(g => g.UserId == _ID)
            .Select(p => new CryptoOrderHistoryDto
            {
                CryptoOrderId = p.CryptoOrderId,
                UserId = p.UserId,
                CryptoId = p.CryptoId,
                OrderPrice = p.OrderPrice,
                Quantity = p.Quantity,
                OrderType = p.OrderType,
                OrderTime = p.OrderTime
            }).ToList();
        }

        public CryptoDto UpdateCryptoPrice(int _ID, decimal _price)
        {
            CryptoDto _ucrypto = _context.Cryptos.Where(g => g.CryptoId == _ID)
            .Select(p => new CryptoDto
            {
                CryptoId = p.CryptoId,
                CurrentPrice = p.CurrentPrice,
                LastUpdate = p.LastUpdate,
                Name = p.Name,
                ShortenedName = p.ShortenedName
            }).FirstOrDefault();
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




        public decimal GetUserCryptoInvestmentSum(int userId)
        {
            decimal _result = _context.CryptoAssets
                                            .Where(s => s.UserId == userId)
                                            .Sum(i => i.BuyPrice);
            return _result;
        }
        
    }
}
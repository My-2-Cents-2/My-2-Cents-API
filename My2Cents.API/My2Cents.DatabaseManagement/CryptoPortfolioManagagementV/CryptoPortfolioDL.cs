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




        public async Task<Decimal> GetUserCryptoInvestmentSum(int userId)
        {
            //for everyone looking at this in the future, this is converted 
            //into double back into decimal because we used SQL Lite for unit
            //testing and SQL lite cannot run the sum function using decimals
            double _result = await _context.CryptoAssets
                                            .Where(s => s.UserId == userId)
                                            .SumAsync(i => (double)i.BuyPrice);
            decimal _decimalResult = Convert.ToDecimal(_result);
            return _decimalResult;
        }
        
    }
}
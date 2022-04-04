using Microsoft.AspNetCore.Mvc;
using My2Cents.API.DataTransferObjects;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
using My2Cents.Logic.Interfaces;

namespace My2Cents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoPortfolioController : ControllerBase
    {
        private readonly ICryptoPortfolioBL _cryptoBL;

        public CryptoPortfolioController(ICryptoPortfolioBL cryptoBL)
        {
            _cryptoBL = cryptoBL;
        }

        /*[HttpPost("AddCrypto")]
        public IActionResult AddNewCrypto(string cryptoName, string shtName, decimal price)
        {
            try
            {
            
                Crypto _newCrypto = new Crypto()
                {
                    CurrentPrice = price,
                    LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                    Name = cryptoName,
                    ShortenedName = shtName
                };
                var _result = _cryptoBL.AddCrypto(_newCrypto);
                //Log.Information("Crypto sessfully added");
                return Created("Has added ", _result);
            }
            catch (System.Exception exe)
            {
                //Log.Warning("Route:" + RouteConfigs.Stock + ": " + exe.Message);
                return BadRequest(exe.Message);
            }
        }*/

        [HttpGet("GetAllCrypto")]
        public IActionResult GetAllCrypto()
        {
            try
            {
                var _result = _cryptoBL.GetAllCrypto();
                //Log.Information("Getting all crypto.");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                
                //Log.Warning(e.Message);
                return NotFound("Failed to get all Crypto");
            }
        
        }

        /*[HttpPost("AddCryptoOrderHistory")]
        public IActionResult AddCryptoOrderHistory(int userID, int cryptoID, decimal orderPrice, decimal quantity)
        {
            try
            {
            
                CryptoOrderHistory _orderhis = new CryptoOrderHistory()
                {
                    UserId = userID,
                    CryptoId = cryptoID,
                    OrderPrice = orderPrice,
                    Quantity = quantity,
                    OrderType = "Buy",
                    OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                };
                var _result = _cryptoBL.AddCryptoOrderHistory(_orderhis);
                //Log.Information("CryptoOrderhistory successfully added");
                return Created("Has added ", _result);
            }
            catch (System.Exception exe)
            {
                //Log.Warning(exe.Message);
                return BadRequest(exe.Message);
            }
        }*/

        [HttpGet("GetCryptoOrderhistorybyUser")]
        public IActionResult GetCryptoOrderHistorybyUser(int _userID)
        {
            try
            {
                var _result = _cryptoBL.GetCryptoOrderHisByUser(_userID);
                //Log.Information("Getting all crypto.");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                
                //Log.Warning(e.Message);
                return NotFound("Failed to get CryptoOrderHistory");
            }
        
        }

        /*[HttpGet("GetCryptoAssetsbyUser")]
        public IActionResult GetCryptoAssetsbyUser(int _userID)
        {
            try
            {
                var _result = _cryptoBL.GetCryptoAssetsByUser(_userID);
                //Log.Information("Getting all crypto.");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                
                //Log.Warning(e.Message);
                return NotFound("Failed to get CryptoAssets");
            }
        
        }

        [HttpGet("GetAllCryptoAssets")]
        public IActionResult GetAllCryptoAssets()
        {
            try
            {
                var _result = _cryptoBL.GetAllCryptoAssets();
                //Log.Information("Getting all crypto.");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                
                //Log.Warning(e.Message);
                return NotFound("Failed to get all CryptoAssets");
            }
        
        }*/

        [HttpGet("GetCryptoAssetTable")]
        public IActionResult GetCryptoAssetTable(int _userID)
        {
            try
            {
                var _result = GetAssetsTable(_userID);
                //Log.Information("Getting all crypto.");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                
                //Log.Warning(e.Message);
                return NotFound("Failed to get CryptoAssets");
            }
        
        }
        
        [HttpGet("GetCryptoOrderhistoryTable")]
        public IActionResult GetCryptoOrderHistoryTable(int _userID)
        {
            try
            {
                var _result = GetOrderHisTable(_cryptoBL.GetCryptoOrderHisByUser(_userID));
                //Log.Information("Getting all crypto.");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                
                //Log.Warning(e.Message);
                return NotFound("Failed to get CryptoOrderHistory");
            }
        
        }

        private List<CryptoAssetTable> GetAssetsTable(int _userID)
        {
            List<CryptoAssetTable> assetTable = new List<CryptoAssetTable>();
            List<CryptoAssetDto> _realCryptoAsset = _cryptoBL.GetCryptoAssetsByUser(_userID);

            foreach (CryptoAssetDto item in _realCryptoAsset)
            {
                CryptoDto _tempCrypto = _cryptoBL.GetCryptoById(item.CryptoId);
                decimal _currentPrice = _tempCrypto.CurrentPrice;
                decimal _quantity = item.Quantity;
                decimal _currentInvestment = item.BuyPrice;
                decimal _totalStockPrice = _quantity * _currentPrice;

                CryptoAssetTable _userCryptoData = new CryptoAssetTable()
                {
                    Name = _tempCrypto.Name,
                    SharePrice = _currentPrice,
                    InitialInvestmentDate = item.BuyDate.ToString("MM/dd/yyyy"),
                    CurrentInvestment = Math.Round(item.BuyPrice, 2, MidpointRounding.ToEven),
                    OwnedShares = Math.Round(_quantity, 2, MidpointRounding.ToEven),
                    Returns = Math.Round(((_currentPrice - _currentInvestment) / (_currentInvestment) ) * 100, 2, MidpointRounding.ToEven),
                    CryptoPrice = Math.Round(_totalStockPrice, 2, MidpointRounding.ToEven)
                };
                assetTable.Add(_userCryptoData);
            }
            return assetTable;
        }

        private List<CryptoOrderHisT> GetOrderHisTable(List<CryptoOrderHistoryDto> oList)
        {
            List<CryptoOrderHisT> _result = new List<CryptoOrderHisT>();
            foreach (CryptoOrderHistoryDto item in oList)
            {
                CryptoOrderHisT _tempOrderHis = new CryptoOrderHisT()
                {
                    Name = _cryptoBL.GetCryptoById(item.CryptoId).Name,
                    CurrentInvestment = Math.Round(item.OrderPrice * item.Quantity, 2, MidpointRounding.ToEven),
                    InitialInvestmentDate = item.OrderTime.ToString("MM/dd/yyyy"),
                    OwnedShares = Math.Round(item.Quantity, 2, MidpointRounding.ToEven),
                    TransactionType = item.OrderType
                };
                _result.Add(_tempOrderHis);
            }

            return _result;

        }

    }
}
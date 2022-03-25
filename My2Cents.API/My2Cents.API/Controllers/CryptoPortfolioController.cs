using Microsoft.AspNetCore.Mvc;
using My2Cents.DataInfrastructure;
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

        [HttpPost("Add Crypto")]
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
        }

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

        [HttpPut("Update Crypto Price")]
        public IActionResult ChangeCryptoPrice(int _ID, decimal _price)
        {
            try
            {
                return Ok(_cryptoBL.UpdateCryptoPrice(_ID, _price));
            }
            catch(System.Exception)
            {
                return NotFound("Failed to find crypto");
            }
        
            
        }

        [HttpPost("Add CryptoOrderHistory")]
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
        }

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

    }
}
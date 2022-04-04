using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My2Cents.Logic;

namespace My2Cents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentPlatformController : ControllerBase
    {
        private IInvesmenentPlatformManagementBL _platformBL; 
        private readonly IConfiguration builder;
        public InvestmentPlatformController(IInvesmenentPlatformManagementBL p_platformBL, IConfiguration builder)
        {
            _platformBL = p_platformBL;
            this.builder = builder;
        }

        // POST: api/InvestmentPlatform/PlaceOrderCrypto
        [HttpPost("PlaceOrderCrypto")]
        public IActionResult PlaceOrderCrypto(int _userID, int _cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.PlaceOrderCrypto(_userID, _cryptoID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        // POST: api/InvestmentPlatform/PlaceOrderCryptoFiat
        [HttpPost("PlaceOrderCryptoFiat")]
        public IActionResult PlaceOrderCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.PlaceOrderCryptoFiat(p_userID, p_cryptoID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        // POST: api/InvestmentPlatform/PlaceOrderStock
        [HttpPost("PlaceOrderStock")]
        public IActionResult PlaceOrderStock(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.PlaceOrderStock(p_userID, p_stockID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        // POST: api/InvestmentPlatform/PlaceOrderStockFiat
        [HttpPost("PlaceOrderStockFiat")]
        public IActionResult PlaceOrderStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.PlaceOrderStockFiat(p_userID, p_stockID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellCrypto
        [HttpPost("SellCrypto")]
        public IActionResult SellCrypto(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.SellCrypto(p_userID, p_cryptoID, amount);
                return Created("Order successfully sold!", _newOrder);

            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellCryptoFiat
        [HttpPost("SellCryptoFiat")]

        public IActionResult SellCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.SellCryptoFiat(p_userID, p_cryptoID, amount);
                return Created("Order successfully sold!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellStock
        [HttpPost("SellStock")]

        public IActionResult SellStock(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.SellStock(p_userID, p_stockID, amount);
                return Created("Order successfully sold!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellStockFiat
        [HttpPost("SellStockFiat")]

        public IActionResult SellStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = _platformBL.SellStockFiat(p_userID, p_stockID, amount);
                return Created("Order successfully sold!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //GET: api/InvestmentPlatform/GetCrypto
        [HttpGet("GetCrypto")]

        public async Task<IActionResult> GetAllCrypto()
        {
            try
            {
                return Ok(await _platformBL.UpdateCryptosData());
            }
            catch (System.Exception e)
            {
                return NotFound("Empty Database");
            }
        }

        //GET: api/InvestmentPlatform/GetStocks
        [HttpGet("GetStocks")]

        public async Task<IActionResult> GetAllStocks()
        {
            //StockApi = builder["StockApiKey"];

             return Ok(await _platformBL.UpdateStocksData());
             
            try
            {
               
            }
            catch (System.Exception e)
            {
                
                return StatusCode(500, e.Message);
            }
        }
    }
}

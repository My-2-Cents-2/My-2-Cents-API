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
        public async Task<IActionResult> PlaceOrderCrypto(int _userID, int _cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.PlaceOrderCrypto(_userID, _cryptoID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        // POST: api/InvestmentPlatform/PlaceOrderCryptoFiat
        [HttpPost("PlaceOrderCryptoFiat")]
        public async Task<IActionResult> PlaceOrderCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.PlaceOrderCryptoFiat(p_userID, p_cryptoID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        // POST: api/InvestmentPlatform/PlaceOrderStock
        [HttpPost("PlaceOrderStock")]
        public async Task<IActionResult> PlaceOrderStock(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.PlaceOrderStock(p_userID, p_stockID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        // POST: api/InvestmentPlatform/PlaceOrderStockFiat
        [HttpPost("PlaceOrderStockFiat")]
        public async Task<IActionResult> PlaceOrderStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.PlaceOrderStockFiat(p_userID, p_stockID, amount);
                return Created("Order successfully placed!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellCrypto
        [HttpPost("SellCrypto")]
        public async Task<IActionResult> SellCrypto(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.SellCrypto(p_userID, p_cryptoID, amount);
                return Created("Order successfully sold!", _newOrder);

            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellCryptoFiat
        [HttpPost("SellCryptoFiat")]

        public async Task<IActionResult> SellCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.SellCryptoFiat(p_userID, p_cryptoID, amount);
                return Created("Order successfully sold!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellStock
        [HttpPost("SellStock")]

        public async Task<IActionResult> SellStock(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.SellStock(p_userID, p_stockID, amount);
                return Created("Order successfully sold!", _newOrder);
            }
            catch (System.Exception e)
            {

                return Conflict(e.Message);
            }
        }

        //POST: api/InvestmentPlatform/SellStockFiat
        [HttpPost("SellStockFiat")]

        public async Task<IActionResult> SellStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            try
            {
                var _newOrder = await _platformBL.SellStockFiat(p_userID, p_stockID, amount);
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
            try
            {
               return Ok(await _platformBL.UpdateStocksData());
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

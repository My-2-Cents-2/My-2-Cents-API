using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.API.Consts;
using My2Cents.API.DataTransferObjects;

namespace My2Cents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockPortfolioController : ControllerBase
    {
        private readonly IStockPortfolioManagementBL _stockPortfolioBL;
        public StockPortfolioController(IStockPortfolioManagementBL s_stockPortfolioBL)
        {
            _stockPortfolioBL = s_stockPortfolioBL;
        }

        // POST: api/Stock
        [HttpPost( RouteConfigs.StockPortfolioStocks )]
        public IActionResult AddNewStock([FromQuery] StockPortfolioStockForm s_stock)
        {
            try
            {
                //_stockBL.ValidStockName(stockName);
                Stock _newStock = new Stock()
                {
                    CurrentPrice = s_stock.CurrentPrice,
                    LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                    Name = s_stock.Name,
                    ShortenedName = s_stock.ShortenedName
                };
                var _result = _stockPortfolioBL.AddNewStock(_newStock);
                //Log.Information("Stock Successfully created");
                return Created("Has created ", _result);
            }
            catch (System.Exception exe)
            {
                //Log.Warning("Route:" + RouteConfigs.Stock + ": " + exe.Message);
                return BadRequest(exe.Message);
            }
        }

        // GET: api/GroupPost
        [HttpGet(RouteConfigs.StockPortfolioStocks)]
        public IActionResult GetAllStocks()
        {
            try
            {
                var _result = _stockPortfolioBL.GetAllStocks();
                //Log.Information("Route: " + RouteConfigs.StockPortfolioStocks);
                //Log.Information("Get All Stocks);

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                //Log.Warning("Route: " + RouteConfigs.StockPortfolioStocks);
                //Log.Warning(e.Message);
                return NotFound("Cannot find any post belongs in this group!");
            }
        }

        // PUT: api/Stock
        [HttpPut(RouteConfigs.StockPortfolioStocks)]
        public IActionResult UpdateStock([FromBody] Stock s_stock)
        {
            try
            {
                var _result = _stockPortfolioBL.UpdateStockInformation(s_stock);
                //Log.Information("Stock Successfully updated");
                return Ok("Stock Updated");
            }
            catch (System.Exception exe)
            {
                //Log.Warning("Route:" + RouteConfigs.Stock + ": " + exe.Message);
                return BadRequest(exe.Message);
            }
        }

//         // DELETE: api/Stock/5
//         [HttpDelete(RouteConfigs.Stock)]
//         public IActionResult DeleteStock(Guid StockID)
//         {
//             try
//             {
//                 Stock _stock = new Stock()
//                 {
//                     StockId = StockID.ToString(),
//                     StockName = ""
//                 };
//                 _stockBL.DeleteStock(_stock);
//                 Log.Information("Stock Successfully deleted");
//                 return Ok("Stock Deleted");
//             }
//             catch (System.Exception exe)
//             {
//                 Log.Warning("Route:" + RouteConfigs.Stock + ": " + exe.Message);
//                 return Conflict(exe.Message);
//             }
//         }
    }
}

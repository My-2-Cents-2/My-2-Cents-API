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

        //PUT: api/Stock
        [HttpPut(RouteConfigs.StockPortfolioStocks)]
        public IActionResult UpdateStockPrice([FromQuery] string stockName, [FromQuery] decimal stockPrice)
        {
            try
            {
                var _result = _stockPortfolioBL.UpdateStockPrice(stockName, stockPrice);
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




        // POST: api/Stock
        [HttpPost( RouteConfigs.StockPortfolioOrders )] 
        public IActionResult AddNewStockOrder([FromQuery] StockPortfolioStockOrderForm s_stockOrder)
        {
            try
            {
                //_stockBL.ValidStockName(stockName);
                //get userId
                //Get stockid
                StockOrderHistory _newStockOrderHistory = new StockOrderHistory()
                {
                    // UserId = s_stockOrder.GetUserId(s_stockOrder.userName);
                    // StockId = s_stockOrder.GetStockId(asdfjkl;);
                    OrderPrice = s_stockOrder.OrderPrice,
                    Quantity = s_stockOrder.Quantity,
                    OrderType = s_stockOrder.OrderType
                };
                var _result = _stockPortfolioBL.AddNewStockOrderHistory(_newStockOrderHistory);
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
        [HttpGet(RouteConfigs.StockPortfolioOrders)]
        public IActionResult GetAllStockOrders()
        {
            try
            {
                var _result = _stockPortfolioBL.GetAllStockOrderHistories();
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

        [HttpGet("RouteConfigs.StockPortfolioOrdersPortfolio")]
        public IActionResult GetUserStockOrderHistoryInformation([FromQuery] int userId)
        {
            try
            {
                
                var _result = _stockPortfolioBL.GetUserStockOrderHistory(userId);
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
/*
        //PUT: api/Stock
        [HttpPut(RouteConfigs.StockPortfolioOrders)]
        public IActionResult UpdateStockOrders([FromBody] Stock s_stock)
        {
            try
            {
                var _result = _stockPortfolioBL.UpdateStockOrderInformation(s_stock);
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
//         public IActionResult DeleteStockOrders(int StockOrderID)
//         {
//             try
//             {
//                 _stockBL.DeleteStockOrderHistory(StockOrderID);
//                 Log.Information("Stock Successfully deleted");
//                 return Ok("Stock Deleted");
//             }
//             catch (System.Exception exe)
//             {
//                 Log.Warning("Route:" + RouteConfigs.Stock + ": " + exe.Message);
//                 return Conflict(exe.Message);
//             }
//         }*/
    }
}

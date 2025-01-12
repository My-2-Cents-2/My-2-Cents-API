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
using My2Cents.DataInfrastructure.Models;

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
                return NotFound(e.Message);
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

        [HttpGet(RouteConfigs.StockPortfolioOrdersPortfolio)]
        public IActionResult GetUserStockOrderHistoryInformation(int userId)
        {
            try
            {
                var _result = ConvertOrderHistoryToForm(_stockPortfolioBL.GetUserStockOrderHistory(userId));
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

        [HttpGet(RouteConfigs.StockPortfolioAssetsPortfolio)]
        public IActionResult GetUserStockPortfolioAssetData(int userId)
        {
            try
            {
                List<StockPortfolioStockInvestmentForm> _result = GetUserStockPortfolioDataFromAssets(userId);
                //Log.Information("Route: " + RouteConfigs.StockPortfolioStocks);
                //Log.Information("Get All Stocks);
                return Ok(_result);
            }
            catch (System.Exception e)
            {
                //Log.Warning("Route: " + RouteConfigs.StockPortfolioStocks);
                //Log.Warning(e.Message);
                return NotFound(e.Message);
//                return NotFound(GetUserStockPortfolioData(int userId));
            }
        }
        private List<StockPortfolioStockInvestmentForm> GetUserStockPortfolioDataFromOrderHistory(int userId)
        {
            //information from stocks
            List<StockPortfolioStockInvestmentForm> pleaseLetThisWork = new List<StockPortfolioStockInvestmentForm>(){};
            List<StockDto> userStocks = _stockPortfolioBL.GetUserStocksFromOrderHistory(userId);
            List<StockOrderHistoryDto> userStockOrderHistory = _stockPortfolioBL.GetUserStockOrderHistory(userId);
            foreach(StockDto aUserStock in userStocks)
            {
                decimal tempTotal = 0;
                // returns = (currentStockPrice - tempCurrentInvestment) / totalInvestment
                decimal tempCurrentInvestment = 0;
                decimal tempOwnedShares = 0;
                foreach(StockOrderHistoryDto order in userStockOrderHistory) {
                    if(order.StockId == aUserStock.StockId)
                    {
                        if(order.OrderType == "buy")
                        {
                            tempOwnedShares += order.Quantity;
                            tempCurrentInvestment += order.Quantity * order.OrderPrice;
                        }
                        else if(order.OrderType == "sell") 
                        {
                            tempOwnedShares -= order.Quantity;
                            tempCurrentInvestment -= order.Quantity * order.OrderPrice;
                        }
                        else 
                        {
                            throw new Exception("A transaction in the table is neither buy nor sell");
                        }
                    }
                };
                StockPortfolioStockInvestmentForm userStockData = new StockPortfolioStockInvestmentForm(){
                    Name =  aUserStock.Name,
                    SharePrice = aUserStock.CurrentPrice,
                    
                    //information from stockorderhistory
                    //get user shares owned from a specific company
                    InitialInvestmentDate = userStockOrderHistory[0].OrderTime.ToString("MM/dd/yyyy"),
                    CurrentInvestment = tempCurrentInvestment,
                    OwnedShares = tempOwnedShares,
                    Returns = ((aUserStock.CurrentPrice * tempOwnedShares  - tempCurrentInvestment) / tempCurrentInvestment ) * 100,
                    StockPrice = tempOwnedShares * aUserStock.CurrentPrice
                };
                pleaseLetThisWork.Add(userStockData);
            }

            return pleaseLetThisWork;
        }
        
        private List<StockPortfolioStockInvestmentForm> GetUserStockPortfolioDataFromAssets(int userId)
        {
            //information from stocks
            List<StockPortfolioStockInvestmentForm> assetTableInformation = new List<StockPortfolioStockInvestmentForm>(){};
            List<StockAssetDto> userStocks = _stockPortfolioBL.GetUserStockAssets(userId);
            foreach(StockAssetDto aUserStock in userStocks)
            {
                // Convert stockId to stock
                StockDto tempStock = _stockPortfolioBL.GetAStockFromId(aUserStock.StockId);
                decimal _currentPrice = tempStock.CurrentPrice;
                decimal _quantity = aUserStock.Quantity;
                decimal _currentInvestment = aUserStock.BuyPrice;
                decimal _totalStockPrice = _quantity * _currentPrice;
                StockPortfolioStockInvestmentForm userStockData = new StockPortfolioStockInvestmentForm(){
                    Name =  tempStock.Name,
                    SharePrice = Math.Round(_currentPrice, 2, MidpointRounding.ToEven ),
                    
                    //information from stockorderhistory
                    //get user shares owned from a specific company
                    InitialInvestmentDate = aUserStock.BuyDate.ToString("MM/dd/yyyy"),
                    CurrentInvestment = Math.Round(aUserStock.BuyPrice, 2, MidpointRounding.ToEven ),
                    OwnedShares = Math.Round(_quantity, 2, MidpointRounding.ToEven ),
                    Returns = Math.Round(((_currentPrice - _currentInvestment) / (_currentInvestment) ) * 100, 2, MidpointRounding.ToEven),
                    StockPrice = Math.Round(_totalStockPrice, 2, MidpointRounding.ToEven )
                };
                assetTableInformation.Add(userStockData);
            }

            return assetTableInformation;
        }
        
        private List<OrderHistoryPortfolioForm> ConvertOrderHistoryToForm(List<StockOrderHistoryDto> o_OrderHistory)
        {
            List<OrderHistoryPortfolioForm> _result = new List<OrderHistoryPortfolioForm>(){};
            foreach(StockOrderHistoryDto _orderHistory in o_OrderHistory)
            {
                OrderHistoryPortfolioForm tempOrderHistoryPortfolioForm = new OrderHistoryPortfolioForm()
                {
                    Name = _stockPortfolioBL.GetAStockFromId(_orderHistory.StockId).Name,
                    CurrentInvestment = Math.Round(_orderHistory.OrderPrice * _orderHistory.Quantity, 2, MidpointRounding.ToEven ),
                    InitialInvestmentDate = _orderHistory.OrderTime.ToString("MM/dd/yyyy") ,
                    OwnedShares = Math.Round(_orderHistory.Quantity, 2, MidpointRounding.ToEven),
                    TransactionType = _orderHistory.OrderType
                };
                _result.Add(tempOrderHistoryPortfolioForm);
            }
            return _result;
        }
    }
}

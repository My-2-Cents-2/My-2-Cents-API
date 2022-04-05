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
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                var _result = await _stockPortfolioBL.GetAllStocks();
                Log.Information("Route: " + RouteConfigs.StockPortfolioStocks);
                Log.Information("Get All Stocks");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.StockPortfolioStocks);
                Log.Warning(e.Message);
                return NotFound(e.Message);
            }
        }

   

        // GET: api/GroupPost
        [HttpGet(RouteConfigs.StockPortfolioOrders)]
        public async Task<IActionResult> GetAllStockOrders()
        {
            try
            {
                var _result = await _stockPortfolioBL.GetAllStockOrderHistories();
                Log.Information("Route: " + RouteConfigs.StockPortfolioOrders);
                Log.Information("Get All Stocks Orders");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.StockPortfolioOrders);
                Log.Warning(e.Message);
                return NotFound("Cannot find any post belongs in this group!");
            }
        }
/*
        [HttpGet(RouteConfigs.StockPortfolioOrdersPortfolio)]
        public async Task<IActionResult> GetUserStockOrderHistoryInformation(int userId)
        {
            try
            {
                Console.WriteLine("Test 1");
                var _data = await _stockPortfolioBL.GetUserStockOrderHistory(userId);
                Console.WriteLine("Test 2");
                var _result = ConvertOrderHistoryToForm(_data);
                Console.WriteLine("Test 4");

                Console.WriteLine(_result);
                
                Log.Information("Route: " + RouteConfigs.StockPortfolioOrdersPortfolio);
                Log.Information("Get user stock order history information");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.StockPortfolioStocks);
                Log.Warning(e.Message);
                return NotFound("Cannot find any post belongs in this group!");
            }
        }*/

        [HttpGet(RouteConfigs.StockPortfolioOrdersPortfolio)]
        public IActionResult GetUserStockOrderHistoryInformationNonAsync(int userId)
        {
            try
            {
                var _data = _stockPortfolioBL.GetUserStockOrderHistoryNonAsync(userId);
                var _result = ConvertOrderHistoryToFormNonAsync(_data);

                Console.WriteLine(_result);
                
                Log.Information("Route: " + RouteConfigs.StockPortfolioOrdersPortfolio);
                Log.Information("Get user stock order history information");

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.StockPortfolioStocks);
                Log.Warning(e.Message);
                return NotFound("Cannot find any post belongs in this group!");
            }
        }

        [HttpGet(RouteConfigs.StockPortfolioAssetsPortfolio)]
        public async Task<IActionResult> GetUserStockPortfolioAssetData(int userId)
        {
            try
            {
                List<StockPortfolioStockInvestmentForm> _result = await GetUserStockPortfolioDataFromAssets(userId);
                Log.Information("Route: " + RouteConfigs.StockPortfolioAssetsPortfolio);
                Log.Information("Get User Stock Portfolio Asset Data");
                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.StockPortfolioStocks);
                Log.Warning(e.Message);
                return NotFound(e.Message);
            }
        }
        private async Task<List<StockPortfolioStockInvestmentForm>> GetUserStockPortfolioDataFromOrderHistory(int userId)
        {
            //information from stocks
            List<StockPortfolioStockInvestmentForm> pleaseLetThisWork = new List<StockPortfolioStockInvestmentForm>(){};
            List<StockDto> userStocks = await _stockPortfolioBL.GetUserStocksFromOrderHistory(userId);
            List<StockOrderHistoryDto> userStockOrderHistory = await _stockPortfolioBL.GetUserStockOrderHistory(userId);
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
        
        private async Task<List<StockPortfolioStockInvestmentForm>> GetUserStockPortfolioDataFromAssets(int userId)
        {
            //information from stocks
            List<StockPortfolioStockInvestmentForm> assetTableInformation = new List<StockPortfolioStockInvestmentForm>(){};
            List<StockAssetDto> userStocks = await _stockPortfolioBL.GetUserStockAssets(userId);
            foreach(StockAssetDto aUserStock in userStocks)
            {
                // Convert stockId to stock
                StockDto tempStock = await _stockPortfolioBL.GetAStockFromId(aUserStock.StockId);
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
        
        private async Task<List<OrderHistoryPortfolioForm>> ConvertOrderHistoryToForm(List<StockOrderHistoryDto> o_OrderHistory)
        {
            List<OrderHistoryPortfolioForm> _result = new List<OrderHistoryPortfolioForm>();
            foreach(StockOrderHistoryDto _orderHistory in o_OrderHistory)
            {
                var stockInfor = await _stockPortfolioBL.GetAStockFromId(_orderHistory.StockId);
                //Console.WriteLine(stockInfor.Name);
                Console.WriteLine("Changed");
                OrderHistoryPortfolioForm tempOrderHistoryPortfolioForm = new OrderHistoryPortfolioForm()
                {
                    Name = "1",
                    CurrentInvestment = Math.Round(_orderHistory.OrderPrice * _orderHistory.Quantity, 2, MidpointRounding.ToEven ),
                    InitialInvestmentDate = _orderHistory.OrderTime.ToString("MM/dd/yyyy") ,
                    OwnedShares = Math.Round(_orderHistory.Quantity, 2, MidpointRounding.ToEven),
                    TransactionType = _orderHistory.OrderType
                };
                _result.Add(tempOrderHistoryPortfolioForm);
            }
            return _result;
        }
        private List<OrderHistoryPortfolioForm> ConvertOrderHistoryToFormNonAsync(List<StockOrderHistoryDto> o_OrderHistory)
        {
            List<OrderHistoryPortfolioForm> _result = new List<OrderHistoryPortfolioForm>();
            foreach(StockOrderHistoryDto _orderHistory in o_OrderHistory)
            {
                var stockInfor = _stockPortfolioBL.GetAStockFromIdNonAsync(_orderHistory.StockId);
                OrderHistoryPortfolioForm tempOrderHistoryPortfolioForm = new OrderHistoryPortfolioForm()
                {
                    Name = stockInfor.Name,
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

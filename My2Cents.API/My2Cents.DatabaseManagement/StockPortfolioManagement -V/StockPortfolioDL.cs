using Microsoft.EntityFrameworkCore;
using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.DatabaseManagement.Implements
{
    public class StockPortfolioManagementDL : IStockPortfolioManagementDL
    {
        private readonly My2CentsContext _context;

        public StockPortfolioManagementDL(My2CentsContext context)
        {
            _context = context;
        }


        public List<StockDto> GetAllStocks()
        {
            List<StockDto> _result = _context.Stocks
                                            .Select(p => new StockDto
                                            {
                                                StockId = p.StockId,
                                                CurrentPrice = p.CurrentPrice,
                                                LastUpdate = p.LastUpdate,
                                                PriceChange = p.PriceChange,
                                                PriceChangePercentage = p.PriceChangePercentage,
                                                Name = p.Name,
                                                ShortenedName = p.ShortenedName
                                            }).ToList();
            if(!_result.Any())
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }

        public StockDto GetAStockFromStockId(int stockId)
        {
            StockDto? _result = _context.Stocks
                                        .Select(p => new StockDto
                                        {
                                            StockId = p.StockId,
                                            CurrentPrice = p.CurrentPrice,
                                            LastUpdate = p.LastUpdate,
                                            PriceChange = p.PriceChange,
                                            PriceChangePercentage = p.PriceChangePercentage,
                                            Name = p.Name,
                                            ShortenedName = p.ShortenedName
                                        }).FirstOrDefault(s => s.StockId == stockId);
            if(_result == null)
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }

        public StockDto GetAStockFromStockName(string stockName)
        {
            StockDto? _result = _context.Stocks
                                        .Select(p => new StockDto
                                        {
                                            StockId = p.StockId,
                                            CurrentPrice = p.CurrentPrice,
                                            LastUpdate = p.LastUpdate,
                                            PriceChange = p.PriceChange,
                                            PriceChangePercentage = p.PriceChangePercentage,
                                            Name = p.Name,
                                            ShortenedName = p.ShortenedName
                                        }).FirstOrDefault(s => s.Name == stockName);
            if(_result == null)
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }

        public List<StockOrderHistoryDto> GetAllStockOrderHistory()
        {
            List<StockOrderHistoryDto> _result = _context.StockOrderHistories
                                                        .Select(p => new StockOrderHistoryDto()
                                                        {
                                                            StockOrderId = p.StockOrderId,
                                                            UserId = p.UserId,
                                                            StockId = p.StockId,
                                                            OrderPrice = p.OrderPrice,
                                                            Quantity = p.Quantity,
                                                            OrderType = p.OrderType,
                                                            OrderTime = p.OrderTime
                                                        }).ToList();
            if(!_result.Any())
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }

        public List<StockOrderHistoryDto> GetUserStockOrders(int userId)
        {
            List<StockOrderHistoryDto> _result = _context.StockOrderHistories
                                                        .Where(s => s.UserId == userId)
                                                        .Select(p => new StockOrderHistoryDto()
                                                        {
                                                            StockOrderId = p.StockOrderId,
                                                            UserId = p.UserId,
                                                            StockId = p.StockId,
                                                            OrderPrice = p.OrderPrice,
                                                            Quantity = p.Quantity,
                                                            OrderType = p.OrderType,
                                                            OrderTime = p.OrderTime
                                                        }).ToList();
            if(!_result.Any())
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }

        public List<StockAssetDto> GetAllStockAssets()
        {
            List<StockAssetDto> _result = _context.StockAssets
                                                    .Select(p => new StockAssetDto()
                                                    {
                                                        StockAssetId = p.StockAssetId,
                                                        StockId = p.StockId,
                                                        UserId = p.UserId,
                                                        BuyPrice = p.BuyPrice,
                                                        BuyDate = p.BuyDate,
                                                        StopLoss = p.StopLoss,
                                                        TakeProfit = p.TakeProfit,
                                                        Quantity = p.Quantity
                                                    }).ToList();
            if(!_result.Any())
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }
        public List<StockAssetDto> GetUserStockAssets(int userId)
        {
            List<StockAssetDto> _result = _context.StockAssets
                                                    .Where(s => s.UserId == userId)
                                                    .Select(p => new StockAssetDto()
                                                    {
                                                        StockAssetId = p.StockAssetId,
                                                        StockId = p.StockId,
                                                        UserId = p.UserId,
                                                        BuyPrice = p.BuyPrice,
                                                        BuyDate = p.BuyDate,
                                                        StopLoss = p.StopLoss,
                                                        TakeProfit = p.TakeProfit,
                                                        Quantity = p.Quantity
                                                    }).ToList();
            if(!_result.Any())
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }

        private StockDto StockToDto(Stock c_stock)
        {
            StockDto _stockDto = new StockDto(){
                StockId = c_stock.StockId,
                CurrentPrice = c_stock.CurrentPrice,
                LastUpdate = c_stock.LastUpdate,
                Name = c_stock.Name,
                ShortenedName = c_stock.ShortenedName

            };
            return _stockDto;
        }
        private StockOrderHistoryDto OrderHistoryToDto(StockOrderHistory c_stockOrderHistory)
        {
            StockOrderHistoryDto _stockOrderHistoryDto = new StockOrderHistoryDto(){
                StockOrderId = c_stockOrderHistory.StockOrderId,
                UserId = c_stockOrderHistory.UserId,
                OrderPrice = c_stockOrderHistory.OrderPrice,
                Quantity = c_stockOrderHistory.Quantity,
                OrderType = c_stockOrderHistory.OrderType,
                OrderTime = c_stockOrderHistory.OrderTime

            };
            return _stockOrderHistoryDto;
        }

        private StockAssetDto StockAssetToDto(StockAsset a_stockAsset)
        {
            StockAssetDto _stockAssetDto = new StockAssetDto(){
                StockAssetId = a_stockAsset.StockAssetId,
                StockId = a_stockAsset.StockId,
                UserId = a_stockAsset.UserId,
                BuyPrice = a_stockAsset.BuyPrice,
                BuyDate = a_stockAsset.BuyDate,
                StopLoss = a_stockAsset.StopLoss,
                TakeProfit = a_stockAsset.TakeProfit,
                Quantity = a_stockAsset.Quantity
            };
            return _stockAssetDto;
        }

        public decimal GetUserStockInvestmentSum(int userId)
        {
            decimal _result = _context.StockAssets
                                            .Where(s => s.UserId == userId)
                                            .Sum(i => i.BuyPrice);
            return _result; 
        }
        
    }
}

using Microsoft.EntityFrameworkCore;
using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DataInfrastructure;

namespace My2Cents.DatabaseManagement.Implements
{
    public class StockPortfolioManagementDL : IStockPortfolioManagementDL
    {
        private readonly My2CentsContext _context;

        public StockPortfolioManagementDL(My2CentsContext context)
        {
            _context = context;
        }

        public Stock AddStock(Stock s_stock)
        {
            Stock newStock = new Stock()
            {
                // should i check for duplicate here or the BL?
                CurrentPrice = s_stock.CurrentPrice,
                LastUpdate = s_stock.LastUpdate,
                Name = s_stock.Name,
                ShortenedName = s_stock.ShortenedName
            };
            _context.Stocks.Add(newStock);
            _context.SaveChanges();

            return newStock;
        }

        public List<Stock> GetAllStocks()
        {
            return _context.Stocks.ToList();
        }

        public Stock GetAStockFromStockId(int stockId)
        {
            return _context.Stocks.FirstOrDefault(s => s.StockId == stockId );
        }

        public Stock GetAStockFromStockName(string stockName)
        {
            return _context.Stocks.FirstOrDefault(s => s.Name == stockName );
        }


        public Stock UpdateStock(Stock s_stock)
        {
            Stock stockToUpdate = _context.Stocks.Where(g => g.StockId == s_stock.StockId).FirstOrDefault();
            if (stockToUpdate != null)
            {
                if (stockToUpdate.CurrentPrice != s_stock.CurrentPrice)
                    stockToUpdate.LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                if (s_stock.CurrentPrice != null)
                    stockToUpdate.CurrentPrice = s_stock.CurrentPrice;
                //if (s_stock.Name != "")
                //    stockToUpdate.Name = s_stock.Name;
                //stockToUpdate.ShortenedName = s_stock.ShortenedName;
            }
            else
            {
                throw new Exception("No stock able to update");
            }

            _context.SaveChanges();
            return stockToUpdate;
        }
        public Stock UpdateStockPrice(int stockId, decimal stockPrice)
        {
            Stock stockToUpdate = _context.Stocks.Where(g => g.StockId == stockId).FirstOrDefault();
            if (stockToUpdate != null)
            {
                if(stockPrice == 0)
                {
                    throw new Exception("price cannot be changed to 0");
                }
                if(stockPrice < 0)
                {
                    throw new Exception("price cannot be less than 0");
                }
                if (stockToUpdate.CurrentPrice != stockPrice)
                    stockToUpdate.LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                if (stockPrice != 0)
                    stockToUpdate.CurrentPrice = stockPrice;
                
            }
            else
            {
                throw new Exception("No stock able to update");
            }

            _context.SaveChanges();
            return stockToUpdate;
        }

        public Stock DeleteStock(int stockId)
        {
            Stock stockToRemove = _context.Stocks.Where(s => (s.StockId == stockId)).FirstOrDefault();
            if (stockToRemove != null)
            {
                _context.Remove(stockToRemove);
                _context.SaveChanges();
                return stockToRemove;
            }
            else
            {
                throw new Exception("Stock not found. Stock could not be deleted.");
            }
        }

        public StockOrderHistory AddStockOrderHistory(StockOrderHistory s_stockOrderHistory)
        {
            StockOrderHistory stockOrderHistory = new StockOrderHistory()
            {
                UserId = s_stockOrderHistory.UserId,
                StockId = s_stockOrderHistory.StockId,
                OrderPrice = s_stockOrderHistory.OrderPrice,
                Quantity = s_stockOrderHistory.Quantity,
                OrderType = s_stockOrderHistory.OrderType,
                OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
            };
            _context.StockOrderHistories.Add(stockOrderHistory);
            _context.SaveChanges();

            return stockOrderHistory;
        }

        public List<StockOrderHistory> GetAllStockOrderHistory()
        {
            return _context.StockOrderHistories.ToList();
        }

        public List<StockOrderHistory> GetUserStockOrders(int userId)
        {
            return _context.StockOrderHistories.Where(s => s.UserId == userId).ToList();
        }

        public StockOrderHistory UpdateStockOrderHistory(StockOrderHistory s_stockOrderHistory)
        {
            StockOrderHistory stockToUpdate = _context.StockOrderHistories.Where(s => s.StockOrderId == s_stockOrderHistory.StockOrderId).FirstOrDefault();
            if (stockToUpdate != null)
            {
                stockToUpdate.UserId = s_stockOrderHistory.UserId;
                stockToUpdate.StockId = s_stockOrderHistory.StockId;
                if(stockToUpdate.OrderPrice != null)
                    stockToUpdate.OrderPrice = s_stockOrderHistory.OrderPrice;
                if(stockToUpdate.Quantity != null)
                    stockToUpdate.Quantity = s_stockOrderHistory.Quantity;
                if(stockToUpdate.OrderType != null)
                    stockToUpdate.OrderType = s_stockOrderHistory.OrderType;
                if(stockToUpdate.OrderTime != null)
                    stockToUpdate.OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            }
            else
            {
                throw new Exception("No stock able to update");
            }

            _context.SaveChanges();
            return stockToUpdate;
        }
        

        public StockOrderHistory DeleteStockOrderHistory(int stockOrderId)
        {
            StockOrderHistory stockOrderHistoryToRemove = _context.StockOrderHistories.Where(s => (s.StockOrderId == stockOrderId)).FirstOrDefault();
            if (stockOrderHistoryToRemove != null)
            {
                _context.Remove(stockOrderHistoryToRemove);
                _context.SaveChanges();
                return stockOrderHistoryToRemove;
            }
            else
            {
                throw new Exception("Stock order history not found. Stock order history could not be deleted.");
            }
        }
    
        public StockAsset AddStockAsset(StockAsset s_stockAsset)
        {
            StockAsset newStockAsset = new StockAsset()
            {
                // add logic to avoid duplicate?
                StockId = s_stockAsset.StockId,
                UserId = s_stockAsset.UserId,
                BuyPrice = s_stockAsset.BuyPrice,
                BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                StopLoss = s_stockAsset.StopLoss,
                TakeProfit = s_stockAsset.TakeProfit,
                Quantity = s_stockAsset.Quantity
                
            };
            _context.StockAssets.Add(newStockAsset);
            _context.SaveChanges();

            return newStockAsset;
        }

        public List<StockAsset> GetAllStockAssets()
        {
            List<StockAsset> _result = _context.StockAssets.ToList();
            if(!_result.Any())
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }
        public List<StockAsset> GetUserStockAssets(int userId)
        {
            List<StockAsset> _result = _context.StockAssets.Where(s => s.UserId == userId).ToList();
            if(!_result.Any())
            {
                throw new Exception("Stocks Asset DNE");
            }
            else
            {
                return _result;
            }
        }


        public StockAsset UpdateStockAsset(StockAsset s_stockAsset)
        {
            StockAsset stockAssetToUpdate = _context.StockAssets.Where(g => g.StockAssetId == s_stockAsset.StockAssetId).FirstOrDefault();
            if (stockAssetToUpdate != null)
            {
                // StockId = s_stockAsset.StockId,
                // UserId = s_stockAsset.UserId,
                // if(s_stockAsset.BuyPrice != null)
                //    stockAssetToUpdate.BuyPrice = s_stockAsset.BuyPrice;
                // BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                if(s_stockAsset.StopLoss != null)
                   stockAssetToUpdate.StopLoss = s_stockAsset.StopLoss;
                if(s_stockAsset.TakeProfit != null)
                    stockAssetToUpdate.TakeProfit = s_stockAsset.TakeProfit;
                if(s_stockAsset.Quantity != null)
                    stockAssetToUpdate.Quantity = s_stockAsset.Quantity;
            }
            else
            {
                throw new Exception("Unavailable assets to update");
            }

            _context.SaveChanges();
            return stockAssetToUpdate;
        }

        public StockAsset DeleteStockAsset(int stockAssetId)
        {
            StockAsset stockAssetToRemove = _context.StockAssets.Where(s => (s.StockAssetId == stockAssetId)).FirstOrDefault();
            if (stockAssetToRemove != null)
            {
                _context.Remove(stockAssetToRemove);
                _context.SaveChanges();
                return stockAssetToRemove;
            }
            else
            {
                throw new Exception("Stock asset not found. Stock asset could not be deleted.");
            }
        }



    }
}

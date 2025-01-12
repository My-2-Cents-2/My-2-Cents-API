using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.Logic.Implements
{
    public class StockPortfolioManagementBL : IStockPortfolioManagementBL
    {
        private readonly IStockPortfolioManagementDL _repo;

        public StockPortfolioManagementBL(IStockPortfolioManagementDL repo)
        {
            _repo = repo;
        }

        public List<StockDto> GetAllStocks()
        {
            List<StockDto> allStocks = _repo.GetAllStocks();
            if (!allStocks.Any())
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return allStocks;
            }
        }

        public StockDto GetAStockFromId(int stockId)
        {
            StockDto allStocks = _repo.GetAStockFromStockId(stockId);
            if (allStocks == null)
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return allStocks;
            }
        }
        
        public List<StockDto> GetUserStocks(int userId)
        {
            List<StockAssetDto> userAssets = _repo.GetUserStockAssets(userId);
            HashSet<StockDto> userStocks = new HashSet<StockDto> ();
            foreach (StockAssetDto asset in userAssets){
                userStocks.Add( _repo.GetAStockFromStockId(asset.StockId));
            }
            if (!userStocks.Any())
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return userStocks.ToList();
            }
        }

        public List<StockDto> GetUserStocksFromOrderHistory(int userId) 
        {
            List<StockOrderHistoryDto> userAssets = _repo.GetUserStockOrders(userId);
            HashSet<StockDto> userStocks = new HashSet<StockDto> ();
            foreach (StockOrderHistoryDto asset in userAssets){
                userStocks.Add( _repo.GetAStockFromStockId(asset.StockId));
            }
            if (!userStocks.Any())
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return userStocks.ToList();
            }
        }

        public bool CheckDuplicateStock(string stockName)
        {
            List<StockDto> _result = _repo.GetAllStocks();
            if (_result.FirstOrDefault(s => s.Name.ToLower() == stockName.ToLower()) == null)
            {
                return true;
            }
            else
            {
                throw new Exception(stockName + " is a duplicate Stock");
            }
        }
        
        public StockDto CheckStockId(int stockId)
        {
            List<StockDto> _result = _repo.GetAllStocks();
            StockDto? stock = _result.FirstOrDefault(s => (s.StockId == stockId));
            if (stock == null)
            {
                throw new Exception("Stock not found from Id");
            }
            else
            {
                return stock;
            }
        }
        
        public int GetStockIdFromName(string stockName)
        {
            try
            {
                int stockId = (_repo.GetAllStocks()
                                .FirstOrDefault(s => (s.Name == stockName))
                                .StockId);
                Console.WriteLine("Test:" + stockName + " " + stockId);
                return stockId;
            }
            catch(System.Exception exe)
            {
                throw new Exception("Stock Name " + stockName + " DNE");
            }
            
        }

        //StockOrderHistory
        public List<StockOrderHistoryDto> GetAllStockOrderHistories()
        {
            List<StockOrderHistoryDto> stockOrderHistories = _repo.GetAllStockOrderHistory();
            if (!stockOrderHistories.Any())
            {
                throw new Exception("No one has any stock orders");
            }
            else
            {
                return stockOrderHistories;
            }
        }

        public List<StockOrderHistoryDto> GetUserStockOrderHistory(int userId)
        {
            try
            {
                return _repo.GetUserStockOrders(userId);
            }
            catch(SystemException exe){
                throw new Exception(exe.Message);
            }
        }
        
        public List<StockAssetDto> GetAllStockAssets()
        {
            try
            {
                return _repo.GetAllStockAssets();
            }
            catch(System.Exception exe )
            {
                throw new Exception(exe.Message);
            }
        }
        
        public List<StockAssetDto> GetUserStockAssets(int userId)
        {
            try
            {
                return _repo.GetUserStockAssets(userId);
            }
            catch(System.Exception exe )
            {
                throw new Exception(exe.Message);
            }
        }

        public decimal GetUserStockInvestmentSum(int userId)
        {
            return _repo.GetUserStockInvestmentSum(userId);
        }

    }
}

using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
// using My2Cents.API.DataTransferObjects;

namespace My2Cents.Logic.Implements
{
    public class StockPortfolioManagementBL : IStockPortfolioManagementBL
    {
        private readonly IStockPortfolioManagementDL _repo;

        public StockPortfolioManagementBL(IStockPortfolioManagementDL repo)
        {
            _repo = repo;
        }

        public async Task<List<StockDto>> GetAllStocks()
        {
            List<StockDto> allStocks = await _repo.GetAllStocks();
            if (!allStocks.Any())
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return allStocks;
            }
        }

        public async Task<List<StockDto>> GetUserStocks(int userId)
        {
            List<StockAssetDto> userAssets = await _repo.GetUserStockAssets(userId);
            HashSet<StockDto> userStocks = new HashSet<StockDto> ();
            foreach (StockAssetDto asset in userAssets){
                userStocks.Add( await _repo.GetAStockFromStockId(asset.StockId));
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
        public async Task<StockDto> GetAStockFromId(int stockId)
        {
            StockDto allStocks = await _repo.GetAStockFromStockId(stockId);
            if (allStocks == null)
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return allStocks;
            }
        }

        public StockDto GetAStockFromIdNonAsync(int stockId)
        {
            StockDto allStocks = _repo.GetAStockFromStockIdNonAsync(stockId);
            return allStocks;
        }
        

        public async Task<List<StockDto>> GetUserStocksFromOrderHistory(int userId) 
        {
            List<StockOrderHistoryDto> userAssets = await _repo.GetUserStockOrders(userId);
            HashSet<StockDto> userStocks = new HashSet<StockDto> ();
            foreach (StockOrderHistoryDto asset in userAssets){
                userStocks.Add( await _repo.GetAStockFromStockId(asset.StockId));
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


        public async Task<bool> CheckDuplicateStock(string stockName)
        {
            List<StockDto> _result = await _repo.GetAllStocks();
            if (_result.FirstOrDefault(s => s.Name.ToLower() == stockName.ToLower()) == null)
            {
                return true;
            }
            else
            {
                throw new Exception(stockName + " is a duplicate Stock");
            }
        }
        public async Task<StockDto> CheckStockId(int stockId)
        {
            List<StockDto> _result = await _repo.GetAllStocks();
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
        /*
        public async Task<int> GetStockIdFromName(string stockName)
        {
            try
            {
                int stockId = (await _repo.GetAStockFromStockName(stockName)).StockId;
                return stockId;
            }
            catch(System.Exception exe)
            {
                throw new Exception("Stock Name " + stockName + " DNE");
            }
            
        }*/



        //StockOrderHistory
        public async Task<List<StockOrderHistoryDto>> GetAllStockOrderHistories()
        {
            List<StockOrderHistoryDto> stockOrderHistories = await _repo.GetAllStockOrderHistory();
            if (!stockOrderHistories.Any())
            {
                throw new Exception("No one has any stock orders");
            }
            else
            {
                return stockOrderHistories;
            }
        }

        public async Task<List<StockOrderHistoryDto>> GetUserStockOrderHistory(int userId)
        {
            try
            {
                return await _repo.GetUserStockOrders(userId);
            }
            catch(SystemException exe){
                throw new Exception(exe.Message);
            }
        }
        

        public List<StockOrderHistoryDto> GetUserStockOrderHistoryNonAsync(int userId)
        {
            return _repo.GetUserStockOrdersNonAsync(userId);
        }

        
        public async Task<List<StockAssetDto>> GetAllStockAssets()
        {
            try
            {
                return await _repo.GetAllStockAssets();
            }
            catch(System.Exception exe )
            {
                throw new Exception(exe.Message);
            }
        }
        public async Task<List<StockAssetDto>> GetUserStockAssets(int userId)
        {
            try
            {
                return await _repo.GetUserStockAssets(userId);
            }
            catch(System.Exception exe )
            {
                throw new Exception(exe.Message);
            }
        }

        public async Task<Decimal> GetUserStockInvestmentSum(int userId)
        {
            return await _repo.GetUserStockInvestmentSum(userId);
        }

    }
}
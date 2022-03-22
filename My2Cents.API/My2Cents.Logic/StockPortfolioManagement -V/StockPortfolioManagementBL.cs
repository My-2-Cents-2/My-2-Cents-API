using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;


namespace My2Cents.Logic.Implements
{
    public class StockPortfolioManagementBL : IStockPortfolioManagementBL
    {
        private readonly IStockPortfolioManagementDL _repo;

        public StockPortfolioManagementBL(IStockPortfolioManagementDL repo)
        {
            _repo = repo;
        }

        public Stock AddNewStock(Stock s_stock)
        {
            List<Stock> _result = _repo.GetAllStocks();
            try
            {
                CheckDuplicateStock(s_stock.Name);
                _repo.AddStock(s_stock);
                return s_stock;
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public List<Stock> GetAllStocks()
        {
            List<Stock> allStocks = _repo.GetAllStocks();
            if (!allStocks.Any())
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return allStocks;
            }
        }
        public Stock UpdateStockInformation(Stock s_stock)
        {
            try
            {
                s_stock.StockId = GetStockIdFromName(s_stock.Name);
                return _repo.UpdateStock(s_stock);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public Stock DeleteStock(int stockId)
        {
            try
            {
                return _repo.DeleteStock(stockId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public bool CheckDuplicateStock(string stockName)
        {
            List<Stock> _result = _repo.GetAllStocks();
            if (_result.FirstOrDefault(s => s.Name.ToLower() == stockName.ToLower()) == null)
            {
                return true;
            }
            else
            {
                throw new Exception(stockName + "Duplicate Stock");
            }
        }
        public Stock CheckStockId(int stockId)
        {
            List<Stock> _result = _repo.GetAllStocks();
            Stock stock = _result.FirstOrDefault(s => (s.StockId == stockId));
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
            int stockId = (_repo.GetAllStocks().FirstOrDefault(s => (s.Name == stockName)).StockId);
            if( stockId == 0)
            {
                Console.WriteLine("Fail: " + stockName + " " + stockId);
                throw new Exception("DNE");
            }
            else{
                Console.WriteLine("Test:" + stockName + " " + stockId);
                return stockId; 
            }
        }



/*
        public Task<Stock> AddNewStock(Stock s_stock)
        {
            List<Stock> _result = _repo.GetAllStocks();
            try
            {
                CheckDuplicateStock(s_stock.Name);
                _repo.AddStock(s_stock);
                return s_stock;
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public Task<List<Stock>> GetAllStocks()
        {
            List<Stock> allStocks = _repo.GetAllStocks();
            if (!allStocks.Any())
            {
                throw new Exception("No one has any stocks");
            }
            else
            {
                return allStocks;
            }
        }
        public Task<Stock> UpdateStockInformation(Stock s_stock)
        {
            try
            {
                CheckStockId(s_stock.StockId);
                return _repo.UpdateStock(s_stock);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public Task<Stock> DeleteStock(int stockId)
        {
            try
            {
                return _repo.DeleteStock(stockId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public Task<bool> CheckDuplicateStock(string stockName)
        {
            List<Stock> _result = _repo.GetAllStocks();
            if (_result.FirstOrDefault(s => s.Name.ToLower() == stockName.ToLower()) == null)
            {
                return true;
            }
            else
            {
                throw new Exception(stockName + "Duplicate Stock");
            }
        }
        public Task<Stock> CheckStockId(int stockId)
        {
            List<Stock> _result = _repo.GetAllStocks();
            Stock stock = _result.FirstOrDefault(s => (s.StockId == stockId));
            if (stock == null)
            {
                throw new Exception("Stock not found from Id");
            }
            else
            {
                return stock;
            }
        }
*/
    }
}
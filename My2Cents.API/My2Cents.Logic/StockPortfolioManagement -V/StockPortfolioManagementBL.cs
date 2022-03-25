using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;
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
        public List<Stock> GetUserStocks(int userId) // i have no clue if this actually works
        {
            List<StockAsset> userAssets = _repo.GetAllStockAssets().Where(s => s.UserId == userId).ToList();
            HashSet<Stock> userStocks = new HashSet<Stock> ();
            foreach (StockAsset asset in userAssets){
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
        public Stock UpdateStockPrice(string stockName, decimal stockPrice)
        {
            try
            {
                int stockId = GetStockIdFromName(stockName);
                return _repo.UpdateStockPrice(stockId, stockPrice);
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
            int stockId = (_repo.GetAllStocks()
                            .FirstOrDefault(s => (s.Name == stockName))
                            .StockId);
            if( stockId == 0)
            {
                Console.WriteLine("Fail: " + stockName + " " + stockId);
                throw new Exception("Stock Name DNE");
            }
            else{
                Console.WriteLine("Test:" + stockName + " " + stockId);
                return stockId; 
            }
        }




        public StockOrderHistory AddNewStockOrderHistory(StockOrderHistory s_stockOrder)
        {
            try
            {
                //StockOrderValidation(s_stockOrder);
                _repo.AddStockOrderHistory(s_stockOrder);
                // need to increase wallet by 1
                return s_stockOrder;
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public List<StockOrderHistory> GetAllStockOrderHistories()
        {
            List<StockOrderHistory> stockOrderHistories = _repo.GetAllStockOrderHistory();
            if (!stockOrderHistories.Any())
            {
                throw new Exception("No one has any stock orders");
            }
            else
            {
                return stockOrderHistories;
            }
        }

        public List<StockOrderHistory> GetUserStockOrderHistory(int userId)
        {
            try
            {
                return _repo.GetUserStockOrders(userId);
            }
            catch(SystemException exe){
                throw new Exception(exe.Message);
            }
        }
/*
        public List<StockPortfolioStockInvestmentForm> GetUserStockPortfolioData(int userId)
        {
            //information from stocks
            StockPortfolioStockInvestmentForm pleaseLetThisWork = new StockPortfolioStockInvestmentForm(){};
            List<Stock> userStocks = GetUserStocks(userId);
            List<StockOrderHistory> userStockOrderHistory = GetUserStockOrderHistory(userId);
            foreach(Stock aUserStock in userStocks)
            {
                decimal tempTotal = 0;
                // returns = (currentStockPrice - tempCurrentInvestment) / totalInvestment
                decimal tempCurrentInvestment = 0;
                decimal tempOwnedShares = 0;
                foreach(StockOrderHistory order in userStockOrderHistory) {
                    if(order.StockId == aUserStock.StockId)
                    {
                        if(order.OrderType ==" buy")
                        {
                            //tempTotal = order.OrderPrice * order.Quantity;

                            //tempCurrentInvestment += 
                            tempOwnedShares += order.Quantity;
                            tempCurrentInvestment += order.Quantity * order.OrderPrice;
                        }
                        else // me assume sell
                        {
                            tempOwnedShares -= order.Quantity;
                            tempCurrentInvestment -= order.Quantity * order.OrderPrice;
                        }
                    }
                };
                StockPortfolioStockInvestmentForm userStockData = new StockPortfolioStockInvestmentForm(){
                    Name = aUserStock.Name,
                    SharePrice = aUserStock.CurrentPrice,
                    
                    //information from stockorderhistory
                    //get user shares owned from a specific company
                    InitialInvestmentDate = userStockOrderHistory[0].OrderTime,
                    currentinvesment = tempCurrentInvestment,
                    ownedShares = tempOwnedShares,
                    StockPrice = userStockOrderHistory[userStockOrderHistory.Count - 1].Quantity * SharePrice
                };
            }
            

            return pleaseLetThisWork;
        }
*/

        public StockOrderHistory UpdateStockOrderInformation(StockOrderHistory s_stockOrder)
        {
            try
            {
                CheckStockId(s_stockOrder.StockId);
                return _repo.UpdateStockOrderHistory(s_stockOrder);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        

        public StockOrderHistory DeleteStockOrderHistory(int stockOrderHistoryId)
        {
            try
            {
                return _repo.DeleteStockOrderHistory(stockOrderHistoryId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        

    }
}
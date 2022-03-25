using My2Cents.DataInfrastructure;
// using My2Cents.API.DataTransferObjects;

namespace My2Cents.Logic.Interfaces
{
    public interface IStockPortfolioManagementBL
    {
        /// <summary>
        /// creates a new stock request
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        Stock AddNewStock(Stock s_stock);
        /// <summary>
        /// returns the list of ALL of the stocks
        /// </summary>
        /// <returns></returns>
        List<Stock> GetAllStocks();
        Stock GetAStockFromId(int stockId);
        /*
        /// <summary>
        /// gets stock info from the stock's name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Stock> GetStockFromName(string stockName);
        */
        /// <summary>
        /// updates the stock status
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        Stock UpdateStockPrice(string stockName, decimal stockPrice);
        /// <summary>
        /// deletes the stock
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        Stock DeleteStock(int stockId);
        bool CheckDuplicateStock(string stockName);
        Stock CheckStockId(int stockId);
        int GetStockIdFromName(string stockName);

        // Stock User

        StockOrderHistory AddNewStockOrderHistory(StockOrderHistory s_stockOrder);
        List<StockOrderHistory> GetAllStockOrderHistories();
        List<StockOrderHistory> GetUserStockOrderHistory(int userId);
        StockOrderHistory UpdateStockOrderInformation(StockOrderHistory s_stockOrder);
        StockOrderHistory DeleteStockOrderHistory(int stockOrderHistoryId);
        
        //StockPortfolioStockInvestmentForm GetUserStockPortfolioData(int userId);
    }
}
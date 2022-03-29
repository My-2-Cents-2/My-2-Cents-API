using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
// using My2Cents.API.DataTransferObjects;

namespace My2Cents.Logic.Interfaces
{
    public interface IStockPortfolioManagementBL
    {
        List<StockDto> GetAllStocks();
        List<StockDto> GetUserStocks(int userId);
        StockDto GetAStockFromId(int stockId);
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
        
        StockDto UpdateStockPrice(string stockName, decimal stockPrice);
        /// <summary>
        /// deletes the stock
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        bool CheckDuplicateStock(string stockName);
        StockDto CheckStockId(int stockId);
        int GetStockIdFromName(string stockName);

        // Stock User

        List<StockOrderHistoryDto> GetAllStockOrderHistories();
        List<StockOrderHistoryDto> GetUserStockOrderHistory(int userId);
        List<StockDto> GetUserStocksFromOrderHistory(int userId);

        //StockAssets
        List<StockAssetDto> GetAllStockAssets();
        List<StockAssetDto> GetUserStockAssets(int userId);
    }
}
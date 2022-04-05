using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
// using My2Cents.API.DataTransferObjects;

namespace My2Cents.Logic.Interfaces
{
    public interface IStockPortfolioManagementBL
    {
        Task<List<StockDto>> GetAllStocks();
        Task<List<StockDto>> GetUserStocks(int userId);
        Task<StockDto> GetAStockFromId(int stockId);
        StockDto GetAStockFromIdNonAsync(int stockId);
        /*
        /// <summary>
        /// gets stock info from the stock's name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Stock> GetStockFromName(string stockName);
        */
        
/*        /// <summary>
        /// updates the stock 
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        StockDto UpdateStockPrice(string stockName, decimal stockPrice); */

        /// <summary>
        /// deletes the stock
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        Task<bool> CheckDuplicateStock(string stockName);
        Task<StockDto> CheckStockId(int stockId);
        //Task<int> GetStockIdFromName(string stockName);

        // Stock User

        Task<List<StockOrderHistoryDto>> GetAllStockOrderHistories();
        Task<List<StockOrderHistoryDto>> GetUserStockOrderHistory(int userId);
        Task<List<StockDto>> GetUserStocksFromOrderHistory(int userId);
        
        //
        List<StockOrderHistoryDto> GetUserStockOrderHistoryNonAsync(int userId);

        //StockAssets
        Task<List<StockAssetDto>> GetAllStockAssets();
        Task<List<StockAssetDto>> GetUserStockAssets(int userId);

        Task<Decimal> GetUserStockInvestmentSum(int userId);
    }
}
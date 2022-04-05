using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.DatabaseManagement.Interfaces
{
    public interface IStockPortfolioManagementDL
    {
        /// <summary>
        /// Retrieves all of the Stocks in a list format
        /// </summary>
        /// <returns></returns>
        Task<List<StockDto>> GetAllStocks();
        /// <summary>
        /// Retrives the information of the specifed stock from the Id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        Task<StockDto> GetAStockFromStockId(int stockId);
        StockDto GetAStockFromStockIdNonAsync(int stockId);
        /// <summary>
        /// Gets the information of a specificed stock from the name
        /// in case of multiple stocks with a similar naming convention 
        /// </summary>
        /// <param name="stockName"></param>
        /// <returns></returns>
        Task<StockDto> GetAStockFromStockName (string stockName);


        // Stock Order Histories

        Task<List<StockOrderHistoryDto>> GetAllStockOrderHistory();
        Task<List<StockOrderHistoryDto>> GetUserStockOrders(int userId);
        List<StockOrderHistoryDto> GetUserStockOrdersNonAsync(int userId);
        
        // Stock Assets


        Task<List<StockAssetDto>> GetAllStockAssets();
        Task<List<StockAssetDto>> GetUserStockAssets(int userId);
//may not be public in the future(start)
        StockDto StockToDto(Stock c_stock);
        StockOrderHistoryDto OrderHistoryToDto(StockOrderHistory c_stockOrderHistory);
        StockAssetDto StockAssetToDto(StockAsset a_stockAsset);
//may not be public in the future(end)
        Task<decimal> GetUserStockInvestmentSum(int userId);
    }
}
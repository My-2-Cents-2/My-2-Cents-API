using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.DatabaseManagement.Interfaces
{
    public interface IStockPortfolioManagementDL
    {
        /// <summary>
        /// Adds the Stock into the database
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        StockDto AddStock(Stock s_stock);
        /// <summary>
        /// Retrieves all of the Stocks in a list format
        /// </summary>
        /// <returns></returns>
        List<StockDto> GetAllStocks();
        /// <summary>
        /// Retrives the information of the specifed stock from the Id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        StockDto GetAStockFromStockId(int stockId);
        /// <summary>
        /// Gets the information of a specificed stock from the name
        /// in case of multiple stocks with a similar naming convention 
        /// </summary>
        /// <param name="stockName"></param>
        /// <returns></returns>
        StockDto GetAStockFromStockName (string stockName);
        /// <summary
        /// Updates the information of the Stock in the database
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        StockDto UpdateStock(Stock s_stock);
        /// <summary>
        /// Deletes a Stock in the database. Only manager can disable stock
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        StockDto UpdateStockPrice(int stockId, decimal stockPrice);

        // Stock Order Histories

        List<StockOrderHistoryDto> GetAllStockOrderHistory();
        List<StockOrderHistoryDto> GetUserStockOrders(int userId);
        
        
        // Stock Assets


        List<StockAssetDto> GetAllStockAssets();
        List<StockAssetDto> GetUserStockAssets(int userId);

        StockAssetDto DeleteStockAsset(int stockAssetId); // need to consider if this should even be a thing because dependencies
    }
}
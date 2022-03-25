using My2Cents.DataInfrastructure;

namespace My2Cents.DatabaseManagement.Interfaces
{
    public interface IStockPortfolioManagementDL
    {
        /// <summary>
        /// Adds the Stock into the database
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        Stock AddStock(Stock s_stock);
        /// <summary>
        /// Retrieves all of the Stocks in a list format
        /// </summary>
        /// <returns></returns>
        List<Stock> GetAllStocks();
        /// <summary>
        /// Retrives the information of the specifed stock from the Id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        Stock GetAStockFromStockId(int stockId);
        /// <summary>
        /// Gets the information of a specificed stock from the name
        /// in case of multiple stocks with a similar naming convention 
        /// </summary>
        /// <param name="stockName"></param>
        /// <returns></returns>
        Stock GetAStockFromStockName (string stockName);
        /// <summary
        /// Updates the information of the Stock in the database
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        Stock UpdateStock(Stock s_stock);
        /// <summary>
        /// Deletes a Stock in the database. Only manager can disable stock
        /// </summary>
        /// <param name="s_stock"></param>
        /// <returns></returns>
        Stock UpdateStockPrice(int stockId, decimal stockPrice);
        Stock DeleteStock(int stockId);

        // Stock Order Histories

        StockOrderHistory AddStockOrderHistory(StockOrderHistory s_stockOrderHistory);
        List<StockOrderHistory> GetAllStockOrderHistory();
        List<StockOrderHistory> GetUserStockOrders(int userId);
        StockOrderHistory UpdateStockOrderHistory(StockOrderHistory s_stockOrderHistory);
        StockOrderHistory DeleteStockOrderHistory(int stockOrderId);
        
        
        // Stock Assets
        StockAsset AddStockAsset(StockAsset s_stockAsset);

        List<StockAsset> GetAllStockAssets();
        List<StockAsset> GetUserStockAssets(int userId);
        StockAsset UpdateStockAsset(StockAsset s_stockAsset);

        StockAsset DeleteStockAsset(int stockAssetId); // need to consider if this should even be a thing because dependencies
    }
}
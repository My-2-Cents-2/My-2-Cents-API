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
        Stock DeleteStock(int stockId);

        // Stock Order Histories

        StockOrderHistory AddStockOrderHistory(StockOrderHistory s_stockOrderHistory);
        List<StockOrderHistory> GetAllStockOrderHistory();
        StockOrderHistory UpdateStockOrderHistory(StockOrderHistory s_stockOrderHistory);
        StockOrderHistory DeleteStockOrderHistory(int stockOrderId);
        
        
        // Stock Assets
        StockAsset AddStockAsset(StockAsset s_stockAsset);

        List<StockAsset> GetAllStockAssets();
        StockAsset UpdateStockAsset(StockAsset s_stockAsset);

        StockAsset DeleteStockAsset(int stockAssetId); // need to consider if this should even be a thing because dependencies
    }
}
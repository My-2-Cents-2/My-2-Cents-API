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
        StockDto GetAStockFromStockName(string stockName);

        // Stock Order Histories
        List<StockOrderHistoryDto> GetAllStockOrderHistory();
        List<StockOrderHistoryDto> GetUserStockOrders(int userId);

        // Stock Assets
        List<StockAssetDto> GetAllStockAssets();
        List<StockAssetDto> GetUserStockAssets(int userId);
        decimal GetUserStockInvestmentSum(int userId);
    }
}

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

        Decimal GetUserStockInvestmentSum(int userId);
    }
}

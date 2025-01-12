namespace My2Cents.API.Consts
{
    public static class RouteConfigs
    {
        //STOCKPROFILE
        public const string StockPortfolioStocks = "Stocks";
        public const string StockPortfolioOrders = "StockOrders";
        public const string StockPortfolioStockAssets = "StockAssets";
        public const string StockPortfolioOrdersPortfolio = "StockOrders/OrderPortfolio/{userId}";
        public const string StockPortfolioAssetsPortfolio = "StockOrders/AssetsPortfolio/{userId}";

        //UserController

        public const string UserInvestmentSum = "UserInvestmentSum/{userId}";
        public const string UserInvestmentValue = "UserInvestmentValue/{userId}";

    }
}
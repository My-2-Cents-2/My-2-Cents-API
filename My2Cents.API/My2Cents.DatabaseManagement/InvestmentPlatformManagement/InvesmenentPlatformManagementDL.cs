using My2Cents.DataInfrastructure;
using RestSharp;
using Newtonsoft.Json;
using My2Cents.DatabaseManagement.Models;
using System.Net;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.DatabaseManagement
{
    public class InvesmenentPlatformManagementDL : IInvesmenentPlatformManagementDL
    {
        private readonly My2CentsContext _context;
        // private readonly HttpClient _httpClient;
        // private readonly JsonSerializerSettings _serializerSettings;

        public InvesmenentPlatformManagementDL(My2CentsContext context)
        {
            _context = context;
            // _httpClient = httpClient;
            // _serializerSettings = serializerSettings;
        }

        // public Account AddMoneytoAccount(Account _balance)
        // {
        //     _context.Accounts.Update(_balance);
        //     _context.SaveChanges();

        //     return _balance;
        // }

        public CryptoOrderHistoryDto PlaceOrderCrypto(int _userID, int _cryptoID, decimal amount)
        {
            //Try to get current asset with our crypto ID and user ID
            var currentAsset = _context.CryptoAssets.FirstOrDefault(p => p.CryptoId.Equals(_cryptoID) &&
                                         p.UserId.Equals(_userID));

            //Check that they have enough money to buy crypto
            var _AccountTypeId = _context.AccountTypes.FirstOrDefault(p => p.AccountType1.Equals("Checking")).AccountTypeId;
            var _CurrentAccount = _context.Accounts.FirstOrDefault(p => p.UserId.Equals(_userID)
            && p.AccountTypeId.Equals(_AccountTypeId));
            var _MoneySpent = amount * _context.Cryptos.FirstOrDefault(p => p.CryptoId == _cryptoID).CurrentPrice;

            if (_CurrentAccount.TotalBalance < _MoneySpent)
            {
                //If you dont have enough it throws new Exception
                throw new Exception("Insufficient Funds");
            }

            //If you have enough money 
            _CurrentAccount.TotalBalance -= _MoneySpent;

            //Create a new crypto order history
            CryptoOrderHistoryDto _newOrder = new CryptoOrderHistoryDto();
            _newOrder.UserId = _userID;
            _newOrder.CryptoId = _cryptoID;
            _newOrder.OrderPrice = _context.Cryptos.FirstOrDefault(p => p.CryptoId.Equals(_cryptoID)).CurrentPrice;
            _newOrder.Quantity = amount;
            _newOrder.OrderType = "Buy";
            _newOrder.OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            _context.CryptoOrderHistories.Add(new CryptoOrderHistory()
            {
                UserId = _newOrder.UserId,
                CryptoId = _newOrder.CryptoId,
                OrderPrice = _newOrder.OrderPrice,
                Quantity = _newOrder.Quantity,
                OrderType = _newOrder.OrderType,
                OrderTime = _newOrder.OrderTime
            });

            //Check if current asset is available in assset table or not 
            if (currentAsset != null)//If its available then run logic 
            {
                currentAsset.Quantity += amount;
                currentAsset.BuyCount += 1;
                _context.SaveChanges();

                return _newOrder;
            }

            //If not then run this logic
            CryptoAsset _newCrypAsset = new CryptoAsset()
            {
                CryptoId = _cryptoID,
                UserId = _userID,
                BuyPrice = _context.Cryptos.FirstOrDefault(p => p.CryptoId.Equals(_cryptoID)).CurrentPrice,
                BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                StopLoss = 0,
                TakeProfit = 0,
                Quantity = amount,
                BuyCount = 1,

            };
            _context.CryptoAssets.Add(_newCrypAsset);


            _context.SaveChanges();

            return _newOrder;
        }

        public CryptoOrderHistoryDto PlaceOrderCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            // //Get the current crypto price
            var _currentCryptoPrice = _context.Cryptos.FirstOrDefault(p => p.CryptoId.Equals(p_cryptoID)).CurrentPrice;

            //Check if we have enough money to buy
            var checkingAccountId = _context.AccountTypes.FirstOrDefault(p => p.AccountType1.Equals("Checking")).AccountTypeId;
            var _currentBalance = _context.Accounts.FirstOrDefault(p => p.UserId.Equals(p_userID)
                                                                        && p.AccountTypeId.Equals(checkingAccountId)).TotalBalance;

            if (_currentBalance < amount)
            {
                //If don't have enough money
                throw new Exception("You don't have enough money to buy!");
            }

            //If have enough money
            var _buyingQuantity = amount / _currentCryptoPrice;



            return PlaceOrderCrypto(p_userID, p_cryptoID, _buyingQuantity);
        }

        public StockOrderHistoryDto PlaceOrderStock(int p_userID, int p_stockID, decimal amount)
        {
            //Try to get current asset with our stock ID and user ID
            var currentAsset = _context.StockAssets.FirstOrDefault(p => p.StockId.Equals(p_stockID) &&
                                         p.UserId.Equals(p_userID));

            //Check that they have enough money to buy stocks
            var _AccountTypeId = _context.AccountTypes.FirstOrDefault(p => p.AccountType1.Equals("Checking")).AccountTypeId;
            var _CurrentAccount = _context.Accounts.FirstOrDefault(p => p.UserId.Equals(p_userID)
            && p.AccountTypeId.Equals(_AccountTypeId));
            var _MoneySpent = amount * _context.Stocks.FirstOrDefault(p => p.StockId == p_stockID).CurrentPrice;


            if (_CurrentAccount.TotalBalance < _MoneySpent)
            {
                //If you dont have enough it throws new Exception
                throw new Exception("Insufficient Funds");
            }

            //If you have enough money 
            _CurrentAccount.TotalBalance -= _MoneySpent;



            //Create a new crypto order history
            StockOrderHistoryDto _newOrder = new StockOrderHistoryDto();
            _newOrder.UserId = p_userID;
            _newOrder.StockId = p_stockID;
            _newOrder.OrderPrice = _context.Stocks.FirstOrDefault(p => p.StockId.Equals(p_stockID)).CurrentPrice;
            _newOrder.Quantity = amount;
            _newOrder.OrderType = "Buy";
            _newOrder.OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            _context.StockOrderHistories.Add(new StockOrderHistory()
            {
                UserId = _newOrder.UserId,
                StockId = _newOrder.StockId,
                OrderPrice = _newOrder.OrderPrice,
                Quantity = _newOrder.Quantity,
                OrderType = _newOrder.OrderType,
                OrderTime = _newOrder.OrderTime
            });

            //Check if current asset is available in assset table or not 
            if (currentAsset != null)//If its available then run logic 
            {
                currentAsset.Quantity += amount;
                currentAsset.BuyCount += 1;
                _context.SaveChanges();

                return _newOrder;
            }

            //If not then run this logic
            StockAsset _newStockAsset = new StockAsset()
            {
                StockId = p_stockID,
                UserId = p_userID,
                BuyPrice = _context.Stocks.FirstOrDefault(p => p.StockId.Equals(p_stockID)).CurrentPrice,
                BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                StopLoss = 0,
                TakeProfit = 0,
                Quantity = amount,
                BuyCount = 1,

            };
            _context.StockAssets.Add(_newStockAsset);


            _context.SaveChanges();

            return _newOrder;
        }

        public StockOrderHistoryDto PlaceOrderStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            // //Get the current stock price
            var _currentStockPrice = _context.Stocks.FirstOrDefault(p => p.StockId.Equals(p_stockID)).CurrentPrice;

            //Check if we have enough money to buy
            var checkingAccountId = _context.AccountTypes.FirstOrDefault(p => p.AccountType1.Equals("Checking")).AccountTypeId;
            var _currentBalance = _context.Accounts.FirstOrDefault(p => p.UserId.Equals(p_userID)
                                                                        && p.AccountTypeId.Equals(checkingAccountId)).TotalBalance;
            if (_currentBalance < amount)
            {
                //If don't have enough money
                throw new Exception("You don't have enough money to buy!");
            }

            //If have enough money
            var _buyingQuantity = amount / _currentStockPrice;

            return PlaceOrderStock(p_userID, p_stockID, _buyingQuantity);
        }

        public CryptoOrderHistoryDto SellCrypto(int _userID, int _cryptoID, decimal amount)
        {
            //Try to get the current Crypto if available
            var _currentCrypto = _context.CryptoAssets.FirstOrDefault(p => p.CryptoId.Equals(_cryptoID)
                                                                        && p.UserId.Equals(_userID));


            //Check if the current crypto is available
            if (_currentCrypto != null)
            {
                //If available, we need to check the amount that they want to sell is not more than the amount they have
                //If not we just throw new exception to tell the client
                if (_currentCrypto.Quantity < amount)
                {
                    throw new Exception("You don't have enough to sell");
                }

                //If the amount is less than so we start the transactions
                //START TRANSACTIONS
                _context.Database.BeginTransaction();

                //Subtract the amount out of the CryptoAsset
                _currentCrypto.Quantity -= amount;

                //Add a new order history in the CryptoOrderHistory
                CryptoOrderHistory _newOrder = new CryptoOrderHistory()
                {
                    UserId = _userID,
                    CryptoId = _cryptoID,
                    OrderPrice = _context.Cryptos.FirstOrDefault(p => p.CryptoId.Equals(_cryptoID)).CurrentPrice,
                    Quantity = amount,
                    OrderType = "Sell",
                    OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };
                _context.Add(_newOrder);

                //Add the money to the wallet
                var checkingacountID = _context.AccountTypes.FirstOrDefault(p => p.AccountType1.Equals("Checking"));
                var _currentCheckingAccount = _context.Accounts.FirstOrDefault(p => p.UserId.Equals(_userID)
                                                                                    && p.AccountTypeId.Equals(checkingacountID));

                //COMMIT TRANSACTIONS
                _context.Database.CommitTransaction();
                _context.SaveChanges();

                CryptoOrderHistoryDto _order = new CryptoOrderHistoryDto()
                {
                    UserId = _newOrder.UserId,
                    CryptoId = _newOrder.CryptoId,
                    OrderPrice = _newOrder.OrderPrice,
                    Quantity = _newOrder.Quantity,
                    OrderType = _newOrder.OrderType,
                    OrderTime = _newOrder.OrderTime
                };

                return _order;
            }

            //If null, throw new exception
            throw new Exception("You don't own any of this crypto!");
        }

        public CryptoOrderHistoryDto SellCryptoFiat(int p_userID, int p_cryptoID, decimal amount)
        {
            var _currentCrypto = _context.CryptoAssets.FirstOrDefault(p => p.UserId.Equals(p_userID)
                                                                        && p.CryptoId.Equals(p_cryptoID));

            //Check if we have enough Crypto amount to sell
            //Get the current price of crypto
            var _cryptoPrice = _context.Cryptos.FirstOrDefault(p => p.CryptoId.Equals(p_cryptoID)).CurrentPrice;
            if (_currentCrypto.Quantity * _cryptoPrice < amount)
            {
                //If don't have enough
                throw new Exception("You don't have enough crypto to sell!");
            }

            //If have enough to sell
            var _sellingQuantity = amount / _cryptoPrice;

            return SellCrypto(p_userID, p_cryptoID, _sellingQuantity);
        }

        public StockOrderHistoryDto SellStock(int p_userID, int p_stockID, decimal amount)
        {
            //Try to get the current Stock if available
            var _currentStock = _context.StockAssets.FirstOrDefault(p => p.StockId.Equals(p_stockID)
                                                                        && p.UserId.Equals(p_userID));


            //Check if the current stock is available
            if (_currentStock != null)
            {
                //If available, we need to check the amount that they want to sell is not more than the amount they have
                //If not we just throw new exception to tell the client
                if (_currentStock.Quantity < amount)
                {
                    throw new Exception("You don't have enough to sell");
                }

                //If the amount is less than so we start the transactions
                //START TRANSACTIONS
                _context.Database.BeginTransaction();

                //Subtract the amount out of the CryptoAsset
                _currentStock.Quantity -= amount;

                //Add a new order history in the CryptoOrderHistory
                StockOrderHistory _newOrder = new StockOrderHistory()
                {
                    UserId = p_userID,
                    StockId = p_stockID,
                    OrderPrice = _context.Stocks.FirstOrDefault(p => p.StockId.Equals(p_stockID)).CurrentPrice,
                    Quantity = amount,
                    OrderType = "Sell",
                    OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };
                _context.Add(_newOrder);

                //Add the money to the wallet
                var checkingacountID = _context.AccountTypes.FirstOrDefault(p => p.AccountType1.Equals("Checking"));
                var _currentCheckingAccount = _context.Accounts.FirstOrDefault(p => p.UserId.Equals(p_userID)
                                                                                    && p.AccountTypeId.Equals(checkingacountID));

                //COMMIT TRANSACTIONS
                _context.Database.CommitTransaction();
                _context.SaveChanges();

                StockOrderHistoryDto _order = new StockOrderHistoryDto()
                {
                    UserId = _newOrder.UserId,
                    StockId = _newOrder.StockId,
                    OrderPrice = _newOrder.OrderPrice,
                    Quantity = _newOrder.Quantity,
                    OrderType = _newOrder.OrderType,
                    OrderTime = _newOrder.OrderTime
                };

                return _order;
            }

            //If null, throw new exception
            throw new Exception("You don't own any of this crypto!");
        }

        public StockOrderHistoryDto SellStockFiat(int p_userID, int p_stockID, decimal amount)
        {
            var _currentStock = _context.StockAssets.FirstOrDefault(p => p.UserId.Equals(p_userID)
                                                                        && p.StockId.Equals(p_stockID));

            //Check if we have enough Stock amount to sell
            //Get the current price of stock
            var _stockPrice = _context.Stocks.FirstOrDefault(p => p.StockId.Equals(p_stockID)).CurrentPrice;
            if (_currentStock.Quantity * _stockPrice < amount)
            {
                //If don't have enough
                throw new Exception("You don't have enough stock to sell!");
            }

            //If have enough to sell
            var _sellingQuantity = amount / _stockPrice;

            return SellStock(p_userID, p_stockID, _sellingQuantity);
        }

        public async Task<List<CryptoDto>> UpdateCryptosData()
        {
            string[] listCryptoID = { "bitcoin", "ethereum", "tether", "binancecoin" };
            if (DateTime.UtcNow.Minute % 10 == 0)
            {

            }
            List<CoinMarkets> _listOfCoinMarkets = await GetCoinMarkets("usd", new string[] { }, "market_cap_desc", 50, 1, false, "1h", null);


            foreach (var crypto in _listOfCoinMarkets)
            {
                //Check if the the crypto is already in the database
                var _currentcryto = _context.Cryptos.FirstOrDefault(p => p.Name.Equals(crypto.Name));
                DateTimeOffset _dateTime = crypto.LastUpdated;
                //If not in the database, add it
                if (_currentcryto == null)
                {
                    _context.Cryptos.Add(new Crypto()
                    {
                        CurrentPrice = crypto.CurrentPrice,
                        LastUpdate = _dateTime.DateTime,
                        Name = crypto.Name,
                        PriceChange = crypto.PriceChange24H,
                        PriceChangePercentage = crypto.PriceChangePercentage24H,
                        ImageURL = crypto.Image.ToString(),
                        ShortenedName = crypto.Symbol,
                        CryptoNameId = crypto.Id
                    });
                    _context.SaveChanges();
                }
                else
                {
                    _currentcryto.CurrentPrice = crypto.CurrentPrice;
                    _currentcryto.LastUpdate = _dateTime.DateTime;
                    _currentcryto.PriceChange = crypto.PriceChange24H;
                    _currentcryto.PriceChangePercentage = crypto.PriceChangePercentage24H;
                    _currentcryto.CryptoNameId = crypto.Id;
                    _currentcryto.ImageURL = crypto.Image.ToString();
                    _context.SaveChanges();
                }
            }

            List<CryptoDto> _CryptoData = _context.Cryptos.Select(p => new CryptoDto
            {
                CryptoId = p.CryptoId,
                CurrentPrice = p.CurrentPrice,
                LastUpdate = p.LastUpdate,
                Name = p.Name,
                ShortenedName = p.ShortenedName,
                ImageURL = p.ImageURL,
                PriceChange = p.PriceChange,
                PriceChangePercentage = p.PriceChangePercentage,
                CryptoNameId = p.CryptoNameId
            }).ToList();

            return _CryptoData;
        }

        public async Task<List<CoinMarkets>> GetCoinMarkets(string vsCurrency, string[] ids, string order, int? perPage,
            int? page, bool sparkline, string priceChangePercentage, string category)
        {
            return await GetAsyncCrypto<List<CoinMarkets>>(QueryStringService.AppendQueryString("coins/markets",
                new Dictionary<string, object>
                {
                    {"vs_currency", vsCurrency},
                    {"ids", string.Join(",", ids)},
                    {"order",order},
                    {"per_page", perPage},
                    {"page", page},
                    {"sparkline", sparkline},
                    {"price_change_percentage", priceChangePercentage},
                    {"category",category}
                })).ConfigureAwait(false);
        }

        public async Task<T> GetAsyncStock<T>(Uri resourceUri)
        {
            //_httpClient.DefaultRequestHeaders.Add("User-Agent", "your bot 0.1");
            HttpClient _httpClient = new HttpClient();

            //Adding API Key to request headers in HttpClient Equivalent to C#
            //Credits to : Stephen Pagdilao, You're welcome
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = resourceUri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("X-API-KEY", "aTxpqYkQjC7BYnhL5IqZf2anrmzswvrM1bBb2xG6");



            var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);


            response.EnsureSuccessStatusCode();

            // var responseContent = await response.Content.ReadAsStringAsync();
            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();
                return JsonConvert.DeserializeObject<T>(responseContent, _serializerSettings);
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }
        }
        public async Task<T> GetAsyncCrypto<T>(Uri resourceUri)
        {
            //_httpClient.DefaultRequestHeaders.Add("User-Agent", "your bot 0.1");
            HttpClient _httpClient = new HttpClient();

            //Adding API Key to request headers in HttpClient Equivalent to C#
            //Credits to : Stephen Pagdilao, You're welcome
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = resourceUri;
            request.Method = HttpMethod.Get;


            var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();
                return JsonConvert.DeserializeObject<T>(responseContent, _serializerSettings);
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

        public async Task<List<StockDto>> UpdateStocksData()
        {
            //string[] listOfRegions = {"us", "eu", "can", "jpn"};
            var _lastUpdated = _context.Stocks.FirstOrDefault().LastUpdate;
            _lastUpdated = _lastUpdated.AddHours(1);
            var _currentTime = DateTime.UtcNow;
            //if (DateTime.UtcNow.Minute == 0 || _lastUpdated.CompareTo(_currentTime) == 1){
            string stockList = "TSLA,AAPL,AMZN,MSFT,NIO,NVDA,MRNA,NKLA,FB,AMD";
            List<MarketDataStock> _listOfStockMarkets = await GetStockMarket(new string[] { }, new string[] { }, stockList);
            foreach (var stock in _listOfStockMarkets[0].Datas)
            {
                //Check if stock is already in the database
                var _currentStock = _context.Stocks.FirstOrDefault(p => p.Name.Equals(stock.Attributes.LongName));

                //DateTime _datetime = stock._dateTime;
                //No LastUpdate time need to discuss what to do/what is needed

                //If not in database, add it
                if (_currentStock == null)
                {
                    _context.Stocks.Add(new Stock()
                    {
                        CurrentPrice = stock.Attributes.RegMarketPrice,
                        Name = stock.Attributes.LongName,
                        ShortenedName = stock.ID,
                        PriceChange = stock.Attributes.RegMarketChange,
                        PriceChangePercentage = stock.Attributes.RegMarketChangePecent,
                        LastUpdate = DateTime.UtcNow
                    });
                    _context.SaveChanges();
                }
                else
                {
                    _currentStock.CurrentPrice = stock.Attributes.RegMarketPrice;
                    _currentStock.PriceChange = stock.Attributes.RegMarketChange;
                    _currentStock.PriceChangePercentage = stock.Attributes.RegMarketChangePecent;
                    _currentStock.LastUpdate = DateTime.UtcNow;
                    _context.SaveChanges();
                }
            }
            //}


            List<StockDto> _StocksData = _context.Stocks.Select(p => new StockDto
            {
                StockId = p.StockId,
                CurrentPrice = p.CurrentPrice,
                LastUpdate = p.LastUpdate,
                Name = p.Name,
                ShortenedName = p.ShortenedName,
                PriceChange = p.PriceChange,
                PriceChangePercentage = p.PriceChangePercentage
            }).ToList();

            return _StocksData;
        }

        public async Task<List<MarketDataStock>> GetStockMarket(string[] Region, string[] lang, string symbols)
        {
            return new List<MarketDataStock>() {await GetAsyncStock<MarketDataStock>(QueryStringServiceStock.AppendQueryString("market/get-realtime-prices",
                new Dictionary<string, object>
                {
                    // {"region", string.Join(",", Region)},
                    // {"lang", string.Join(",", lang)},
                    {"symbols", symbols}
                })).ConfigureAwait(false)};
        }

        public static class QueryStringService
        {
            public static Uri AppendQueryString(string path, Dictionary<string, object> parameter)
            {
                return CreateUrl(path, parameter);
            }
            public static Uri AppendQueryString(string path)
            {
                return CreateUrl(path, new Dictionary<string, object>());
            }
            private static Uri CreateUrl(string path, Dictionary<string, object> parameter)
            {
                var urlParameters = new List<string>();
                foreach (var par in parameter)
                {
                    urlParameters.Add(par.Value == null || string.IsNullOrWhiteSpace(par.Value.ToString())
                        ? null
                        : $"{par.Key}={par.Value.ToString().ToLower()}");
                }

                var encodedParams = urlParameters
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(WebUtility.HtmlEncode)
                    .Select((x, i) => i > 0 ? $"&{x}" : $"?{x}")
                    .ToArray();
                var url = encodedParams.Length > 0 ? $"{path}{string.Join(string.Empty, encodedParams)}" : path;
                return new Uri(BaseApiEndPointUrl.ApiEndPoint, url);
            }
        }

        public static class QueryStringServiceStock
        {
            public static Uri AppendQueryString(string path, Dictionary<string, object> parameter)
            {
                return CreateUrl(path, parameter);
            }
            public static Uri AppendQueryString(string path)
            {
                return CreateUrl(path, new Dictionary<string, object>());
            }
            private static Uri CreateUrl(string path, Dictionary<string, object> parameter)
            {
                var urlParameters = new List<string>();
                foreach (var par in parameter)
                {
                    urlParameters.Add(par.Value == null || string.IsNullOrWhiteSpace(par.Value.ToString())
                        ? null
                        : $"{par.Key}={par.Value.ToString().ToLower()}");
                }

                var encodedParams = urlParameters
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(WebUtility.HtmlEncode)
                    .Select((x, i) => i > 0 ? $"&{x}" : $"?{x}")
                    .ToArray();
                var url = encodedParams.Length > 0 ? $"{path}{string.Join(string.Empty, encodedParams)}" : path;
                return new Uri(BaseApiEndPointUrl.StockEndPoint, url);
            }
        }

        public static class BaseApiEndPointUrl
        {
            public static readonly Uri ApiEndPoint = new Uri("https://api.coingecko.com/api/v3/");
            // public static readonly Uri StockEndPoint = new Uri("https://yfapi.net/v6/");
            public static readonly Uri StockEndPoint = new Uri("https://alpha.financeapi.net/");
        }

        // public CryptoAsset BuyExistingCrypto(CryptoAsset _asset)
        // {
        //     _context.CryptoAssets.Update(_asset);
        //     _context.SaveChanges();

        //     return _asset;
        // }

        // public StockAsset BuyExistingStock(StockAsset _asset)
        // {
        //     _context.StockAssets.Update(_asset);
        //     _context.SaveChanges();

        //     return _asset;
        // }

        // public StockAsset BuyStock(StockAsset _asset)
        // {
        //     var currentAsset = _context.StockAssets.FirstOrDefault(p => p.StockId.Equals(_asset.StockId) &&
        //                                     p.UserId.Equals(_asset.UserId));
        //     if (currentAsset != null)
        //     {
        //         _context.StockAssets.Update(_asset);
        //         _context.SaveChanges();
        //         return _asset;
        //     }
        //     _context.StockAssets.Add(_asset);
        //     _context.SaveChanges();

        //     return _asset;
        // }

        // public CryptoAsset SellCrypto(CryptoAsset _asset)
        // {
        //     _context.CryptoAssets.Update(_asset);
        //     _context.SaveChanges();

        //     return _asset;
        // }

        // public StockAsset SellStock(StockAsset _asset)
        // {
        //     var currentAsset = _context.CryptoAssets.FirstOrDefault(p => p.CryptoId.Equals(_cryptoID) &&
        //                                 p.UserId.Equals(_userID));

        //     if (currentAsset != null)
        //     {
        //         _context.StockAssets.Update(_asset);
        //         _context.SaveChanges();

        //         return _asset;
        //     }


        // }

        // public Account SubtractFromAccount(Account _balance)
        // {
        //     _context.Accounts.Update(_balance);
        //     _context.SaveChanges();

        //     return _balance;
        // }
    }
}
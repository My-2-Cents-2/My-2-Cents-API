using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models ;
using My2Cents.DatabaseManagement;
using System.Threading.Tasks;

namespace StockPortfolioManagementTest
{
    public class DbContextRepositoryTest
    {
        private readonly DbContextOptions<My2CentsContext> options;
        private readonly DbContextOptions<My2CentsContext> options2;

        public DbContextRepositoryTest()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = TestStockPortfolio.db").Options;
            SeedStockPortfolioDL();
            options2 = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = TestEmptyStockPortfolio.db").Options;
            SeedEmptyStockPortfolioDL();
        }

        [Fact]
        async Task GetAllStocks()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockDto> listOfStocks = await _repo.GetAllStocks();

                //Assert
                Assert.Equal(2, listOfStocks.Count);
                Assert.Equal(1, listOfStocks[0].StockId);
                Assert.Equal(100, listOfStocks[0].CurrentPrice);
                Assert.Equal(1, listOfStocks[0].PriceChange);
                Assert.Equal(1, listOfStocks[0].PriceChangePercentage);
                Assert.Equal("Rhongobongo", listOfStocks[0].Name);
                Assert.Equal("RHBO", listOfStocks[0].ShortenedName);
                Assert.Equal("VFDS", listOfStocks[1].ShortenedName);
            }
        }
        [Fact]
        async Task Fail_Get_All_Stocks()
        {
            using (My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.ThrowsAsync<Exception>(() => _repo.GetAllStocks() );
            }
        }

        [Fact]
        async Task Get_A_Stock_From_Stock_Id()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                StockDto _stock = await _repo.GetAStockFromStockId(2);

                //Assert
                Assert.Equal(2, _stock.StockId);
                Assert.Equal(200, _stock.CurrentPrice);
                Assert.Equal(2, _stock.PriceChange);
                Assert.Equal(2, _stock.PriceChangePercentage);
                Assert.Equal("VeryFunDragonsactions", _stock.Name);
                Assert.Equal("VFDS", _stock.ShortenedName);
            }
        }

        [Fact]
        async Task Fail_Get_A_Stock_From_Stock_Id_Because_Id_DNE()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.ThrowsAsync<Exception>( () => _repo.GetAStockFromStockId(3) );
            }
        }

        [Fact]
        void Get_A_Stock_From_Stock_Id_NonAsync()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                StockDto _stock = _repo.GetAStockFromStockIdNonAsync(2);

                //Assert
                Assert.Equal(2, _stock.StockId);
                Assert.Equal(200, _stock.CurrentPrice);
                Assert.Equal(2, _stock.PriceChange);
                Assert.Equal(2, _stock.PriceChangePercentage);
                Assert.Equal("VeryFunDragonsactions", _stock.Name);
                Assert.Equal("VFDS", _stock.ShortenedName);
            }
        }

        [Fact]
        async Task Get_A_Stock_From_Stock_Name()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                StockDto _stock = await _repo.GetAStockFromStockName("Rhongobongo");

                //Assert
                Assert.Equal(1, _stock.StockId);
                Assert.Equal(100, _stock.CurrentPrice);
                Assert.Equal(1, _stock.PriceChange);
                Assert.Equal(1, _stock.PriceChangePercentage);
                Assert.Equal("Rhongobongo", _stock.Name);
                Assert.Equal("RHBO", _stock.ShortenedName);
            }
        }

        [Fact]
        async Task Fail_Get_A_Stock_Because_DNE()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.ThrowsAsync<Exception>( () => _repo.GetAStockFromStockName("DDDynamite") );
            }
        }

        [Fact]
        async Task Get_All_Order_History()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockOrderHistoryDto> listOfOrderHistories = await _repo.GetAllStockOrderHistory();

                //Assert
                Assert.Equal(2, listOfOrderHistories.Count);
                Assert.Equal(1, listOfOrderHistories[0].StockId);
                Assert.Equal(1, listOfOrderHistories[0].UserId);
                Assert.Equal(100, listOfOrderHistories[0].OrderPrice);
                Assert.Equal(2, listOfOrderHistories[0].Quantity);
                Assert.Equal("buy", listOfOrderHistories[0].OrderType);
            }
        }
        
        [Fact]
        async Task Fail_Get_All_Order_History()
        {
            using (My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.ThrowsAsync<Exception>(() => _repo.GetAllStockOrderHistory() );
            }
        }

        [Fact]
        async Task Get_User_Stock_Order()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                var stockOrders = await _repo.GetUserStockOrders(1);

                //Assert
                Assert.Equal(1,stockOrders[0].StockOrderId);
                Assert.Equal(1,stockOrders[0].UserId);
                Assert.Equal(1,stockOrders[0].StockId);
                Assert.Equal(100,stockOrders[0].OrderPrice);
                Assert.Equal(2,stockOrders[0].Quantity);
                Assert.Equal("buy",stockOrders[0].OrderType);
            }
        }

        [Fact]
        async Task Fail_Get_User_Stock_Order()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.ThrowsAsync<Exception>( () => _repo.GetUserStockOrders(2) );
            }
        }

        [Fact]
        void Get_User_Stock_Order_NonAsync()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                var stockOrders = _repo.GetUserStockOrdersNonAsync(1);

                //Assert
                Assert.Equal(1,stockOrders[0].StockOrderId);
                Assert.Equal(1,stockOrders[0].UserId);
                Assert.Equal(1,stockOrders[0].StockId);
                Assert.Equal(100,stockOrders[0].OrderPrice);
                Assert.Equal(2,stockOrders[0].Quantity);
                Assert.Equal("buy",stockOrders[0].OrderType);
            }
        }

        [Fact]
        void Fail_Get_User_Stock_Order_NonAsync()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>( () => _repo.GetUserStockOrdersNonAsync(2) );
            }
        }

        [Fact]
        async Task Get_Stock_Assests()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockAssetDto> _result = await _repo.GetAllStockAssets();

                //Assert
                Assert.Equal(2,_result.Count);
                Assert.Equal(2,_result[1].StockAssetId);
                Assert.Equal(2,_result[1].StockId);
                Assert.Equal(1,_result[1].UserId);
                Assert.Equal(200,_result[1].BuyPrice);
                Assert.Equal(0,_result[1].StopLoss);
                Assert.Equal(9001,_result[1].TakeProfit);
                Assert.Equal(3,_result[1].Quantity);
            }
        }

        [Fact]
        async Task Fail_Get_Stock_Assets()
        {
            using(My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.ThrowsAsync<Exception>( () => _repo.GetAllStockAssets());
            }
        }
        
        [Fact]
        async Task Get_User_Stock_Assets()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockAssetDto> _result = await _repo.GetUserStockAssets(1);

                //Assert
                Assert.Equal(2,_result[1].StockAssetId);
                Assert.Equal(2,_result[1].StockId);
                Assert.Equal(1,_result[1].UserId);
                Assert.Equal(200,_result[1].BuyPrice);
                Assert.Equal(0,_result[1].StopLoss);
                Assert.Equal(9001,_result[1].TakeProfit);
                Assert.Equal(3,_result[1].Quantity);
            }
        }

        [Fact]
        async Task Fail_Get_User_Stock_Assets()
        {
            using(My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.ThrowsAsync<Exception>( () => _repo.GetUserStockAssets(2));
            }
        }       


        [Fact]
        public void Stock_To_Dto()
        {
            using(My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                Stock _testStock = new Stock
                {
                    StockId = 0,
                    CurrentPrice = 100,
                    LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                    PriceChange = 1,
                    PriceChangePercentage = 1,
                    Name = "Rhongobongo",
                    ShortenedName = "RHBO"
                };
                StockDto _testStockDto = new StockDto
                {
                    StockId = _testStock.StockId,
                    CurrentPrice = _testStock.CurrentPrice,
                    LastUpdate = _testStock.LastUpdate,
                    PriceChange = _testStock.PriceChange,
                    PriceChangePercentage = _testStock.PriceChangePercentage,
                    Name = _testStock.Name,
                    ShortenedName = _testStock.ShortenedName
                };

                //Act
                StockDto _actualStockDto = _repo.StockToDto(_testStock);
                
                //Assert
                Assert.Equal(_testStockDto.Name, _actualStockDto.Name); 
            }
        }

        [Fact]
        public void OrderHistory_To_Dto()
        {
            using(My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                StockOrderHistory _testStockOrderHistory = new StockOrderHistory{
                    StockOrderId = 0,
                    UserId = 1,
                    StockId = 1,
                    OrderPrice = 100,
                    Quantity = 2,
                    OrderType = "buy",
                    OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };

                StockOrderHistoryDto _testStockOrderHistoryDto = new StockOrderHistoryDto{
                    StockOrderId = 0,
                    UserId = 1,
                    StockId = 1,
                    OrderPrice = 100,
                    Quantity = 2,
                    OrderType = "buy",
                    OrderTime = _testStockOrderHistory.OrderTime
                };

                //Act
                StockOrderHistoryDto _actualStockOrderHistoryDto = _repo.OrderHistoryToDto(_testStockOrderHistory);
                
                //Assert
                Assert.Equal(_testStockOrderHistoryDto.StockId, _actualStockOrderHistoryDto.StockId); 
            }
        }


        [Fact]
        public void Stock_Asset_To_Dto()
        {
            using(My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                

                StockAsset _testStockAsset = new StockAsset
                {
                    StockAssetId = 0,
                    StockId = 1,
                    UserId = 1,
                    BuyPrice = 100,
                    BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                    StopLoss = 0,
                    TakeProfit = 9001,
                    Quantity = 2                        
                };

                StockAssetDto _testStockAssetDto = new StockAssetDto
                {
                    StockAssetId = 0,
                    StockId = 1,
                    UserId = 1,
                    BuyPrice = 100,
                    BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                    StopLoss = 0,
                    TakeProfit = 9001,
                    Quantity = 2                        
                };



                //Act
                StockAssetDto _actualStockAssetDto = _repo.StockAssetToDto(_testStockAsset);
                
                //Assert
                Assert.Equal(_testStockAssetDto.StockId, _actualStockAssetDto.StockId);
            }
        }

        
        [Fact]
        async Task Get_User_Investment_Sum()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                decimal _result = await _repo.GetUserStockInvestmentSum(1);

                //Assert
                Assert.Equal(300,_result);
            }
        }

        private void SeedStockPortfolioDL()
        {
            using(My2CentsContext context = new My2CentsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Users.Add(
                    new ApplicationUser{
                        Id = 1,
                        UserName = "TestUserName",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = true,
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    }
                );

                context.Stocks.AddRange(
                    new Stock{
                        StockId = 1,
                        CurrentPrice = 100,
                        LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                        PriceChange = 1,
                        PriceChangePercentage = 1,
                        Name = "Rhongobongo",
                        ShortenedName = "RHBO"
                    },
                    new Stock{
                        StockId = 2,
                        CurrentPrice = 200,
                        LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                        PriceChange = 2,
                        PriceChangePercentage = 2,
                        Name = "VeryFunDragonsactions",
                        ShortenedName = "VFDS"
                    }
                );

                context.StockOrderHistories.AddRange(
                    new StockOrderHistory{
                        StockOrderId = 1,
                        UserId = 1,
                        StockId = 1,
                        OrderPrice = 100,
                        Quantity = 2,
                        OrderType = "buy",
                        OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                    },
                    new StockOrderHistory{
                        StockOrderId = 2,
                        UserId = 1,
                        StockId = 2,
                        OrderPrice = 200,
                        Quantity = 3,
                        OrderType = "buy",
                        OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                    }
                );

                context.StockAssets.AddRange(
                    new StockAsset{
                        StockAssetId = 1,
                        StockId = 1,
                        UserId = 1,
                        BuyPrice = 100,
                        BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                        StopLoss = 0,
                        TakeProfit = 9001,
                        Quantity = 2                        
                    },
                    new StockAsset{
                        StockAssetId = 2,
                        StockId = 2,
                        UserId = 1,
                        BuyPrice = 200,
                        BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                        StopLoss = 0,
                        TakeProfit = 9001,
                        Quantity = 3                       
                    }
                ); 
                context.SaveChanges();
            }
        }
        private void SeedEmptyStockPortfolioDL()
        {
            using(My2CentsContext context2 = new My2CentsContext(options2))
            {
                context2.Database.EnsureDeleted();
                context2.Database.EnsureCreated();
                
                context2.SaveChanges();
            }
        }
    }
}

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
        void GetAllStocks()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockDto> listOfStocks = _repo.GetAllStocks();

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
        void Fail_Get_All_Stocks()
        {
            using (My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>(() => _repo.GetAllStocks() );
            }
        }

        [Fact]
        void Get_A_Stock_From_Stock_Id()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                StockDto _stock = _repo.GetAStockFromStockId(2);

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
        void Fail_Get_A_Stock_From_Stock_Id_Because_Id_DNE()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>( () => _repo.GetAStockFromStockId(3) );
            }
        }

        [Fact]
        void Get_A_Stock_From_Stock_Name()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                StockDto _stock = _repo.GetAStockFromStockName("Rhongobongo");

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
        void Fail_Get_A_Stock_Because_DNE()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>( () => _repo.GetAStockFromStockName("DDDynamite") );
            }
        }

        [Fact]
        void Get_All_Order_History()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockOrderHistoryDto> listOfOrderHistories = _repo.GetAllStockOrderHistory();

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
        void Fail_Get_All_Order_History()
        {
            using (My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>(() => _repo.GetAllStockOrderHistory() );
            }
        }

        [Fact]
        void Get_User_Stock_Order()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                var asdf = _repo.GetUserStockOrders(1);

                //Assert
                Assert.Equal(1,asdf[0].StockOrderId);
                Assert.Equal(1,asdf[0].UserId);
                Assert.Equal(1,asdf[0].StockId);
                Assert.Equal(100,asdf[0].OrderPrice);
                Assert.Equal(2,asdf[0].Quantity);
                Assert.Equal("buy",asdf[0].OrderType);
            }
        }

        [Fact]
        void Fail_Get_User_Stock_Order()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>( () => _repo.GetUserStockOrders(2) );
            }
        }

        [Fact]
        void Get_Stock_Assests()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockAssetDto> _result = _repo.GetAllStockAssets();

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
        void Fail_Get_Stock_Assets()
        {
            using(My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>( () => _repo.GetAllStockAssets());
            }
        }
        
        [Fact]
        void Get_User_Stock_Assets()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                List<StockAssetDto> _result = _repo.GetUserStockAssets(1);

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
        void Fail_Get_User_Stock_Assets()
        {
            using(My2CentsContext context = new My2CentsContext(options2))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>( () => _repo.GetUserStockAssets(2));
            }
        }       
        
        /*
        [Fact]
        void Pass_Stock_To_Dto()
        {
            using(My2CentsContext context = new My2CentsContext(options2))
            {
                //Act
                Stock testStock = new Stock()
                {
                    StockId = 2,
                    CurrentPrice = 200,
                    LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                    PriceChange = 2,
                    PriceChangePercentage = 2,
                    Name = "VeryFunDragonsactions",
                    ShortenedName = "VFDS"
                };

                //Act
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);
                StockDto _result =  _repo.StockToDto(testStock);

                //Assert
                Assert.IsType(StockDto, _result);
            }
        }*/
/*
        [Fact]
        void Get_User_Investment_Sum()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act
                decimal _result = _repo.GetUserStockInvestmentSum(1);

                //Assert
                Assert.Equal(2,_result);
            }
        }
*/
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

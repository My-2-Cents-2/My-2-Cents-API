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

        public DbContextRepositoryTest()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = TestStockPortfolio.db").Options;
            SeedStockPortfolioDL();
        }

        [Fact]
        void Get_All_Stocks()
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
                Assert.Equal("Rhongobongo", listOfStocks[0].Name);
                Assert.Equal("VFDS", listOfStocks[1].ShortenedName);
            }
        }
/*
        [Fact]
        void Get_All_Stocks()
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
                Assert.Equal("Rhongobongo", listOfStocks[0].Name);
                Assert.Equal("VFDS", listOfStocks[1].ShortenedName);
            }
        } */

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
                        Name = "Rhongobongo",
                        ShortenedName = "RHBO"
                    },
                    new Stock{
                        StockId = 2,
                        CurrentPrice = 999,
                        LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                        Name = "VeryFunDragonsactions",
                        ShortenedName = "VFDS"
                    }
                );

                context.StockOrderHistories.AddRange(
                    new StockOrderHistory{
                        StockOrderId = 1,
                        UserId = 1,
                        StockId = 1,
                        OrderPrice = 172,
                        Quantity = 2,
                        OrderType = "buy",
                        OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                    }
                );

                context.StockAssets.AddRange(
                    new StockAsset{
                        StockAssetId = 1,
                        StockId = 1,
                        UserId = 1,
                        BuyPrice = 172,
                        BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
                        StopLoss = 0,
                        TakeProfit = 9001,
                        Quantity = 2                        
                    }
                ); 
                context.SaveChanges();
            }
        }
    }
}
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

namespace StockPortfolioManagementTest
{
    public class DbContextRepositoryTest
    {
        private readonly DbContextOptions<My2CentsContext> options;

        public DbContextRepositoryTest()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = Test.db").Options;
            Seed();
        }

        private void Seed()
        {
            using(My2CentsContext context = new My2CentsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

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

                context.StockOrderHistory.AddRange(
                    new Account{
                        StockOrderId = 1,
                        UserId = 1,
                        StockId = 1,
                        OrderPrice = 172,
                        Quantity = 2,
                        OrderType = "buy",
                        OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                    }
                );

                context.StockOrderHistory.AddRange(
                    new Account{
                        StockOrderId = 1,
                        UserId = 1,
                        StockId = 1,
                        OrderPrice = 172,
                        Quantity = 2,
                        OrderType = "buy",
                        OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                    }
                );
            }
        }
    }
}

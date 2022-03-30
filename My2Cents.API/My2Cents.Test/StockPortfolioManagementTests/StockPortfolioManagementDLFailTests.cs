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
    public class StockPortfolioManagementDLFailTests
    {
        private readonly DbContextOptions<My2CentsContext> options;

        public StockPortfolioManagementDLFailTests()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = TestEmptyStockPortfolio.db").Options;
            SeedEmptyStockPortfolioDL();
        }

        [Fact]
        void Fail_Get_All_Stocks()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IStockPortfolioManagementDL _repo = new StockPortfolioManagementDL(context);

                //Act & Assert
                Assert.Throws<Exception>(() => _repo.GetAllStocks() );
            }
        }


        private void SeedEmptyStockPortfolioDL()
        {
            using(My2CentsContext context = new My2CentsContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.SaveChanges();
            }
        }
    }
}

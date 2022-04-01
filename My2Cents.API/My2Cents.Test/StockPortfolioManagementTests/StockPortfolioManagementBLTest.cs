using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models ;
using My2Cents.DatabaseManagement;


public class StockPortfolioManagementBLTest
{
    [Fact]
    public void Should_Get_All_Stocks()
    {
        //Arrange
        Stock _testStock = new Stock
        {
            StockId = 1,
            CurrentPrice = 100,
            LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            PriceChange = 1,
            PriceChangePercentage = 1,
            Name = "Rhongobongo",
            ShortenedName = "RHBO"
        };
        StockDto _testStockDto = new StockDto
        {
            StockId = 1,
            CurrentPrice = 100,
            LastUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            PriceChange = 1,
            PriceChangePercentage = 1,
            Name = "Rhongobongo",
            ShortenedName = "RHBO"
        };

        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();
        _expectedListOfStocksDto.Add(_testStockDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        List<StockDto> _actualListOfStockDto = _stockBL.GetAllStocks();
        
        //Assert
        Assert.Same(_expectedListOfStocksDto, _actualListOfStockDto);
    }
}
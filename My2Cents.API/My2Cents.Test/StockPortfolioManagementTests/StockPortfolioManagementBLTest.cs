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
            LastUpdate = _testStock.LastUpdate,
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

    [Fact]
    public void Should_Fail_Get_All_Stocks()
    {
        //Arrange
        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act & Assert 
        Assert.Throws<Exception>( ()=> _stockBL.GetAllStocks() );
    }
    
    [Fact]
    public void Should_Get_A_Stock_From_Stock_Id()
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
            LastUpdate = _testStock.LastUpdate,
            PriceChange = 1,
            PriceChangePercentage = 1,
            Name = "Rhongobongo",
            ShortenedName = "RHBO"
        };

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAStockFromStockId(1)).Returns(_testStockDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        StockDto _actualStockDto = _stockBL.GetAStockFromId(1);
        
        //Assert
        Assert.Same(_testStockDto, _actualStockDto);
    }

    [Fact]
    public void Should_Fail_Get_A_Stock_From_Stock_Id()
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

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAStockFromStockId(1)).Returns(_testStockDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act & Assert
        Assert.Throws<Exception>( () => _stockBL.GetAStockFromId(3) );
    }

    [Fact]
    public void Should_Get_A_User_Stocks()
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
            LastUpdate = _testStock.LastUpdate,
            PriceChange = 1,
            PriceChangePercentage = 1,
            Name = "Rhongobongo",
            ShortenedName = "RHBO"
        };

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();
        _expectedListOfStocksDto.Add(_testStockDto);

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        _expectedListOfStocksAssetsDto.Add(_testStockAssetDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        _mockRepo.Setup(repo => repo.GetAStockFromStockId(1)).Returns(_testStockDto);
        _mockRepo.Setup(repo => repo.GetUserStockAssets(1)).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        List<StockDto> _actualListOfStockDto = _stockBL.GetUserStocks(1);
        
        //Assert
        Assert.Equal(_expectedListOfStocksDto, _actualListOfStockDto);
    }

    [Fact]
    public void Should_Fail_Get_A_User_Stocks()
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
            LastUpdate = _testStock.LastUpdate,
            PriceChange = 1,
            PriceChangePercentage = 1,
            Name = "Rhongobongo",
            ShortenedName = "RHBO"
        };

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = _testStockAsset.BuyDate,
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();
        //_expectedListOfStocksDto.Add(_testStockDto);

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        //_expectedListOfStocksAssetsDto.Add(_testStockAssetDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        _mockRepo.Setup(repo => repo.GetAStockFromStockId(1)).Returns(_testStockDto);
        _mockRepo.Setup(repo => repo.GetUserStockAssets(1)).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act & Assert
        Assert.Throws<Exception>(() => _stockBL.GetUserStocks(1));
    }

    

    [Fact]
    public void Should_Get_User_Stocks_From_Order_History()
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

        StockOrderHistory _testStockOrderHistory = new StockOrderHistory{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        StockOrderHistoryDto _testStockOrderHistoryDto = new StockOrderHistoryDto{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = _testStockOrderHistory.OrderTime
        };

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();
        _expectedListOfStocksDto.Add(_testStockDto);

        List<StockOrderHistoryDto> _expectedListOfStockOrderHistoryDto = new List<StockOrderHistoryDto>();
        _expectedListOfStockOrderHistoryDto.Add(_testStockOrderHistoryDto);

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        _expectedListOfStocksAssetsDto.Add(_testStockAssetDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        _mockRepo.Setup(repo => repo.GetAStockFromStockId(1)).Returns(_testStockDto);
        _mockRepo.Setup(repo => repo.GetUserStockOrders(1)).Returns(_expectedListOfStockOrderHistoryDto);
        _mockRepo.Setup(repo => repo.GetUserStockAssets(1)).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        List<StockDto> _actualListOfStockDto = _stockBL.GetUserStocksFromOrderHistory(1);
        
        //Assert
        Assert.Equal(_expectedListOfStocksDto, _actualListOfStockDto);
    }

    [Fact]
    public void Should_Fail_Get_A_User_Stock_Order_History()
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

        StockOrderHistory _testStockOrderHistory = new StockOrderHistory{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        StockOrderHistoryDto _testStockOrderHistoryDto = new StockOrderHistoryDto{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = _testStockOrderHistory.OrderTime
        };

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();
        _expectedListOfStocksDto.Add(_testStockDto);

        List<StockOrderHistoryDto> _expectedListOfStockOrderHistoryDto = new List<StockOrderHistoryDto>();
        //_expectedListOfStockOrderHistoryDto.Add(_testStockOrderHistoryDto);

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        _expectedListOfStocksAssetsDto.Add(_testStockAssetDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        _mockRepo.Setup(repo => repo.GetAStockFromStockId(1)).Returns(_testStockDto);
        _mockRepo.Setup(repo => repo.GetUserStockOrders(1)).Returns(_expectedListOfStockOrderHistoryDto);
        _mockRepo.Setup(repo => repo.GetUserStockAssets(1)).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);
 
        //Act & Assert
        Assert.Throws<Exception>(() => _stockBL.GetUserStocksFromOrderHistory(1));
    }



    [Fact]
    public void Check_Duplicate_Stock()
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
            LastUpdate = _testStock.LastUpdate,
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
        bool _result = _stockBL.CheckDuplicateStock("NotADupe");
        
        //Assert
        Assert.True(_result);
    }

    [Fact]
    public void Fail_Check_Duplicate_Stock()
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
            LastUpdate = _testStock.LastUpdate,
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

        //Act & Assert 
        Assert.Throws<Exception>( ()=> _stockBL.CheckDuplicateStock("Rhongobongo") );
    }

    [Fact]
    public void Get_Stock_Id_From_Name()
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
            LastUpdate = _testStock.LastUpdate,
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
        int _result = _stockBL.GetStockIdFromName("Rhongobongo");
        
        //Assert
        Assert.Equal(1, _result);
    }

    [Fact]
    public void Fail_Get_Stock_Id_From_Name()
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
            LastUpdate = _testStock.LastUpdate,
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

        //Act & Assert 
        Assert.Throws<Exception>( ()=> _stockBL.GetStockIdFromName("NonexistantStock") );
    }
    


    [Fact]
    public void Should_Get_All_Stock_Order_History()
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

        StockOrderHistory _testStockOrderHistory = new StockOrderHistory{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        StockOrderHistoryDto _testStockOrderHistoryDto = new StockOrderHistoryDto{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = _testStockOrderHistory.OrderTime
        };

        

        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();
        _expectedListOfStocksDto.Add(_testStockDto);

        List<StockOrderHistoryDto> _expectedListOfStockOrderHistoryDto = new List<StockOrderHistoryDto>();
        _expectedListOfStockOrderHistoryDto.Add(_testStockOrderHistoryDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        _mockRepo.Setup(repo => repo.GetAllStockOrderHistory()).Returns(_expectedListOfStockOrderHistoryDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        List<StockOrderHistoryDto> _actualListOfStockDto = _stockBL.GetAllStockOrderHistories();
        
        //Assert
        Assert.Same(_expectedListOfStockOrderHistoryDto, _actualListOfStockDto);
    }

    [Fact]
    public void Should_Fail_Get_All_Stock_Order_History()
    {

        StockOrderHistory _testStockOrderHistory = new StockOrderHistory{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        StockOrderHistoryDto _testStockOrderHistoryDto = new StockOrderHistoryDto{
            StockOrderId = 1,
            UserId = 1,
            StockId = 1,
            OrderPrice = 100,
            Quantity = 2,
            OrderType = "buy",
            OrderTime = _testStockOrderHistory.OrderTime
        };

        List<StockOrderHistoryDto> _expectedListOfStockOrderHistoryDto = new List<StockOrderHistoryDto>();
        
        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStockOrderHistory()).Returns(_expectedListOfStockOrderHistoryDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);
 
        //Act & Assert
        Assert.Throws<Exception>(() => _stockBL.GetAllStockOrderHistories() );
    }

    [Fact]
    public void Should_Get_All_Stock_Assets()
    {
        //Arrange

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        _expectedListOfStocksAssetsDto.Add(_testStockAssetDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStockAssets()).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        List<StockAssetDto> _actualListOfStockAssetsDto = _stockBL.GetAllStockAssets();
        
        //Assert
        Assert.Same(_expectedListOfStocksAssetsDto, _actualListOfStockAssetsDto);
    }
/*
    [Fact]
    public void Should_Fail_Get_All_Stock_Assets()
    {
        //Arrange

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        
        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStockAssets()).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act & Assert
        Assert.Throws<Exception>( () => _stockBL.GetAllStockAssets());
    }
*/
    [Fact]
    public void Should_Get_User_Stock_Assets()
    {
        //Arrange

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        _expectedListOfStocksAssetsDto.Add(_testStockAssetDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetUserStockAssets(1)).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        List<StockAssetDto> _actualListOfStockAssetsDto = _stockBL.GetUserStockAssets(1);
        
        //Assert
        Assert.Same(_expectedListOfStocksAssetsDto, _actualListOfStockAssetsDto);
    }
/*
    [Fact]
    public void Should_Fail_Get_User_Stock_Assets()
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
        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        }; 


        List<StockDto> _expectedListOfStocksDto = new List<StockDto>();
        _expectedListOfStocksDto.Add(_testStockDto);

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        
        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllStocks()).Returns(_expectedListOfStocksDto);
        _mockRepo.Setup(repo => repo.GetUserStockAssets(1)).Returns(_expectedListOfStocksAssetsDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act & Assert
        Assert.Throws<Exception>( () => _stockBL.GetUserStockAssets(1));
    }
*/
    [Fact]
    public void Should_Get_User_Stock_Investment_Sum()
    {
        //Arrange

        StockAsset _testStockAsset = new StockAsset
        {
            StockAssetId = 1,
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
            StockAssetId = 1,
            StockId = 1,
            UserId = 1,
            BuyPrice = 100,
            BuyDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")),
            StopLoss = 0,
            TakeProfit = 9001,
            Quantity = 2                        
        };

        List<StockAssetDto> _expectedListOfStocksAssetsDto = new List<StockAssetDto>();
        _expectedListOfStocksAssetsDto.Add(_testStockAssetDto);

        Mock<IStockPortfolioManagementDL> _mockRepo = new Mock<IStockPortfolioManagementDL>();
        _mockRepo.Setup(repo => repo.GetUserStockInvestmentSum(1)).Returns(200);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act
        decimal _result = _stockBL.GetUserStockInvestmentSum(1);
        
        //Assert
        Assert.Equal(200, _result);
    }

    
/*
    [Fact]
    public void Should_Fail_Get_A_Stock_From_Stock_Id()
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
        _mockRepo.Setup(repo => repo.GetAStockFromStockId(1)).Returns(_testStockDto);
        IStockPortfolioManagementBL _stockBL = new StockPortfolioManagementBL(_mockRepo.Object);

        //Act & Assert
        Assert.Throws<Exception>( () => _stockBL.GetAStockFromId(3) );
    }*/






}
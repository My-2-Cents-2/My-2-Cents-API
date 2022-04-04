using System.Collections.Generic;
using Moq;
using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
using My2Cents.Logic.Interfaces;
using Xunit;

namespace My2Cents.Test
{
    public class CryptoBLTestsV
    {

        [Fact]
        public void should_get_all_crypto()
        {
            //Arrange
            string _name = "bitcoin";
            string _shortened = "BTC";
            decimal currentPrice = 100;

            CryptoDto crypto = new CryptoDto()
            {
                Name = _name,
                ShortenedName = _shortened,
                CurrentPrice = currentPrice
            };

            List<CryptoDto> ExpectedCryptoList = new List<CryptoDto>();
            ExpectedCryptoList.Add(crypto);

            Mock<ICryptoPortfolioDL> mockRepo = new Mock<ICryptoPortfolioDL>();

            mockRepo.Setup(repo => repo.GetAllCrypto()).Returns(ExpectedCryptoList);

            ICryptoPortfolioBL cusBL = new CryptoPortfolioBL(mockRepo.Object);

            //Act
            List<CryptoDto> actualCustList = cusBL.GetAllCrypto();

            //Assert
            Assert.Same(ExpectedCryptoList, actualCustList);
            Assert.Equal(ExpectedCryptoList[0].Name, actualCustList[0].Name);
            Assert.Equal(ExpectedCryptoList[0].ShortenedName, actualCustList[0].ShortenedName);
            Assert.Equal(ExpectedCryptoList[0].CurrentPrice, actualCustList[0].CurrentPrice);
        }

         [Fact]
        public void should_get_all_crypto_by_id()
        {
            //Arrange
            string _name = "bitcoin";
            string _shortened = "BTC";
            decimal currentPrice = 100;
            int cryptoid = 1;

            CryptoDto crypto = new CryptoDto()
            {
                CryptoId = cryptoid,
                Name = _name,
                ShortenedName = _shortened,
                CurrentPrice = currentPrice
            };

           // List<CryptoDto> ExpectedCryptoList = new List<CryptoDto>();
           // ExpectedCryptoList.Add(crypto);

            Mock<ICryptoPortfolioDL> mockRepo = new Mock<ICryptoPortfolioDL>();

            mockRepo.Setup(repo => repo.GetCryptoById(cryptoid)).Returns(crypto);

            ICryptoPortfolioBL cusBL = new CryptoPortfolioBL(mockRepo.Object);

            //Act
            CryptoDto actualCustList = cusBL.GetCryptoById(cryptoid);

            //Assert
            Assert.Equal(crypto.Name, actualCustList.Name);
            Assert.Equal(crypto.ShortenedName, actualCustList.ShortenedName);
            Assert.Equal(crypto.CurrentPrice, actualCustList.CurrentPrice);
        }

        [Fact]
        public void should_get_all_orderhis_by_id()
        {
            //Arrange
            string _ordertype = "sell";
            decimal _quantity = 1;
            decimal orderPrice = 100;
            int cryptoid = 1;

            CryptoOrderHistoryDto crypto = new CryptoOrderHistoryDto()
            {
                CryptoId = cryptoid,
                OrderType = _ordertype,
                OrderPrice = orderPrice,
                Quantity =_quantity
            };

            List<CryptoOrderHistoryDto> ExpectedCryptoList = new List<CryptoOrderHistoryDto>();
            ExpectedCryptoList.Add(crypto);

            Mock<ICryptoPortfolioDL> mockRepo = new Mock<ICryptoPortfolioDL>();

            mockRepo.Setup(repo => repo.GetCryptoOrderHisByUser(cryptoid)).Returns(ExpectedCryptoList);

            ICryptoPortfolioBL cusBL = new CryptoPortfolioBL(mockRepo.Object);

            //Act
            List<CryptoOrderHistoryDto> actualCustList = cusBL.GetCryptoOrderHisByUser(cryptoid);

            //Assert
            Assert.Same(ExpectedCryptoList, actualCustList);
            Assert.Equal(ExpectedCryptoList[0].CryptoId, actualCustList[0].CryptoId);
            Assert.Equal(ExpectedCryptoList[0].OrderType, actualCustList[0].OrderType);
            Assert.Equal(ExpectedCryptoList[0].Quantity, actualCustList[0].Quantity);
        }

        [Fact]
        public void should_get_all_assets()
        {
            //Arrange
            decimal buyPrice = 100;
            decimal quantity = 2;
            int buyCount = 3;

            CryptoAssetDto crypto = new CryptoAssetDto()
            {
                BuyPrice = buyPrice,
                Quantity = quantity,
                BuyCount = buyCount
            };

            List<CryptoAssetDto> ExpectedCryptoList = new List<CryptoAssetDto>();
            ExpectedCryptoList.Add(crypto);

            Mock<ICryptoPortfolioDL> mockRepo = new Mock<ICryptoPortfolioDL>();

            mockRepo.Setup(repo => repo.GetAllCryptoAssets()).Returns(ExpectedCryptoList);

            ICryptoPortfolioBL cusBL = new CryptoPortfolioBL(mockRepo.Object);

            //Act
            List<CryptoAssetDto> actualCustList = cusBL.GetAllCryptoAssets();

            //Assert
            Assert.Same(ExpectedCryptoList, actualCustList);
            Assert.Equal(ExpectedCryptoList[0].BuyPrice, actualCustList[0].BuyPrice);
            Assert.Equal(ExpectedCryptoList[0].Quantity, actualCustList[0].Quantity);
            Assert.Equal(ExpectedCryptoList[0].BuyCount, actualCustList[0].BuyCount);
        }

        [Fact]
        public void should_get_all_assets_by_id()
        {
            //Arrange
            decimal buyPrice = 100;
            decimal quantity = 2;
            int buyCount = 3;
            int userId = 1;
            CryptoAssetDto crypto = new CryptoAssetDto()
            {
                BuyPrice = buyPrice,
                Quantity = quantity,
                BuyCount = buyCount,
                UserId = userId
            };

            List<CryptoAssetDto> ExpectedCryptoList = new List<CryptoAssetDto>();
            ExpectedCryptoList.Add(crypto);

            Mock<ICryptoPortfolioDL> mockRepo = new Mock<ICryptoPortfolioDL>();

            mockRepo.Setup(repo => repo.GetCryptoAssetsByUser(userId)).Returns(ExpectedCryptoList);

            ICryptoPortfolioBL cusBL = new CryptoPortfolioBL(mockRepo.Object);

            //Act
            List<CryptoAssetDto> actualCustList = cusBL.GetCryptoAssetsByUser(userId);

            //Assert
            Assert.Same(ExpectedCryptoList, actualCustList);
            Assert.Equal(ExpectedCryptoList[0].BuyPrice, actualCustList[0].BuyPrice);
            Assert.Equal(ExpectedCryptoList[0].Quantity, actualCustList[0].Quantity);
            Assert.Equal(ExpectedCryptoList[0].BuyCount, actualCustList[0].BuyCount);
        }

        [Fact]
        public void should_get_add_orderhis()
        {
            //Arrange
            string _ordertype = "sell";
            decimal _quantity = 1;
            decimal orderPrice = 100;
            int cryptoid = 1;

            CryptoOrderHistory crypto = new CryptoOrderHistory()
            {
                CryptoId = cryptoid,
                OrderType = _ordertype,
                OrderPrice = orderPrice,
                Quantity =_quantity
            };

            CryptoOrderHistoryDto cryptoo = new CryptoOrderHistoryDto()
            {
                CryptoId = cryptoid,
                OrderType = _ordertype,
                OrderPrice = orderPrice,
                Quantity =_quantity
            };

            Mock<ICryptoPortfolioDL> mockRepo = new Mock<ICryptoPortfolioDL>();

            mockRepo.Setup(repo => repo.AddCryptoOrderHistory(crypto)).Returns(cryptoo);

            ICryptoPortfolioBL cusBL = new CryptoPortfolioBL(mockRepo.Object);

            //Act
            CryptoOrderHistoryDto actualCustList = cusBL.AddCryptoOrderHistory(crypto);

            //Assert
            Assert.Equal(crypto.CryptoId, actualCustList.CryptoId);
            Assert.Equal(crypto.OrderType, actualCustList.OrderType);
            Assert.Equal(crypto.Quantity, actualCustList.Quantity);
        }
    }

   
    
}
using Microsoft.AspNetCore.Mvc;
using Moq;
using My2Cents.API.Controllers;
using My2Cents.DatabaseManagement;
using My2Cents.DataInfrastructure.Models;
using My2Cents.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace My2Cents.Test {
    public class InvestmentPlatformBLTest {

        [Fact]
        public async Task CryptoOrderShouldBePlaced()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            CryptoOrderHistoryDto _actualOrder = await _investPlatBL.PlaceOrderCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task CryptoOrderShouldNotBePlacedDueToBalance()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("Insufficient Funds"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.PlaceOrderCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task CryptoSellShouldNotBePlacedDueToBalance()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't have enough to sell"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.SellCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task CryptoSellShouldNotBePlacedDueToNoPosition()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't own any of this crypto!"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.SellCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task StockSellShouldNotBePlacedDueToNoPosition()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't own any of this crypto!"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.SellStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task CryptoFiatSellShouldNotBePlacedDueToBalance()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't have enough crypto to sell!"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.SellCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task CryptoFiatOrderShouldNotBePlacedDueToBalance()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't have enough money to buy!"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.PlaceOrderCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task StockOrderShouldNotBePlacedDueToBalance()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("Insufficient Funds"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.PlaceOrderStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task StockFiatOrderShouldNotBePlacedDueToBalance()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't have enough money to buy!"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.PlaceOrderStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task StockSellShouldNotBePlacedDueToBalance()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't have enough to sell"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.SellStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task StockFiatSellShouldNotBePlacedDueToBalance()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ThrowsAsync(new Exception("You don't have enough to sell"));
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(
                async () => await _investPlatBL.SellStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)
            );
        }

        [Fact]
        public async Task StockShouldPlaceOrder()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            StockOrderHistoryDto _actualOrder = await _investPlatBL.PlaceOrderStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task CryptoFiatOrderShouldBePlaced()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            CryptoOrderHistoryDto _actualOrder = await _investPlatBL.PlaceOrderCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task StockFiatShouldPlaceOrder()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Buy",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.PlaceOrderStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            StockOrderHistoryDto _actualOrder = await _investPlatBL.PlaceOrderStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task CryptoSellOrderShouldBePlaced()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            CryptoOrderHistoryDto _actualOrder = await _investPlatBL.SellCrypto(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task StockSellOrderShouldBePlaced()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            StockOrderHistoryDto _actualOrder = await _investPlatBL.SellStock(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task CryptoFiatSellOrderShouldBePlaced()
        {
            // Arrange
            CryptoOrderHistoryDto _expectedOrder = new CryptoOrderHistoryDto() {
                CryptoId = 1,
                CryptoOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            CryptoOrderHistoryDto _actualOrder = await _investPlatBL.SellCryptoFiat(_expectedOrder.UserId, _expectedOrder.CryptoId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task StockFiatSellOrderShouldBePlaced()
        {
            // Arrange
            StockOrderHistoryDto _expectedOrder = new StockOrderHistoryDto() {
                StockId = 1,
                StockOrderId = 1,
                OrderPrice = 100,
                OrderTime = DateTime.UtcNow,
                OrderType = "Sell",
                Quantity = 1,
                UserId = 1
            };

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.SellStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity)).ReturnsAsync(_expectedOrder);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            // Act
            StockOrderHistoryDto _actualOrder = await _investPlatBL.SellStockFiat(_expectedOrder.UserId, _expectedOrder.StockId, _expectedOrder.Quantity);
        
            // Assert
            Assert.Same(_expectedOrder, _actualOrder);
        }

        [Fact]
        public async Task ShouldUpdateCryptoData()
        {
            //Arrange
            CryptoDto _dto = new CryptoDto() {
                CryptoId = 1,
                CurrentPrice = 1,
                LastUpdate = DateTime.UtcNow,
                ImageURL = "testingURL1",
                PriceChange = 1,
                PriceChangePercentage = 1,
                Name = "Crypto1",
                ShortenedName = "Cry1",
                CryptoNameId = "CryptoNameID1"
            };
            List<CryptoDto> expected = new List<CryptoDto>();
            expected.Add(_dto);

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.UpdateCryptosData()).ReturnsAsync(expected);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            //Act
            List<CryptoDto> actual = await _investPlatBL.UpdateCryptosData();

            //Assert
            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task ShouldUpdateStockData()
        {
            //Arrange
            StockDto _dto = new StockDto() {
                StockId = 1,
                CurrentPrice = 1,
                LastUpdate = DateTime.UtcNow,
                PriceChange = 1,
                PriceChangePercentage = 1,
                Name = "Crypto1",
                ShortenedName = "Cry1"
            };
            List<StockDto> expected = new List<StockDto>();
            expected.Add(_dto);

            Mock<IInvesmenentPlatformManagementDL> _mockRepo = new Mock<IInvesmenentPlatformManagementDL>();
            _mockRepo.Setup(repo => repo.UpdateStocksData()).ReturnsAsync(expected);
            IInvesmenentPlatformManagementBL _investPlatBL = new InvesmenentPlatformManagementBL(_mockRepo.Object);

            //Act
            List<StockDto> actual = await _investPlatBL.UpdateStocksData();

            //Assert
            Assert.Same(expected, actual);
        }
    }
    
}
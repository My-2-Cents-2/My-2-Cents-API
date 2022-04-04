using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
using Xunit;
namespace My2Cents.Test
{
    public class CryptoDLTestsV
    {
        private readonly DbContextOptions<My2CentsContext> options;

        public CryptoDLTestsV()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = TestVCrypto.db").Options;
            Seed();
        }

        [Fact]
        void ShouldGetAllCrypto()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                ICryptoPortfolioDL repo = new CryptoPortfolioDL(context);

                //Act
                List<CryptoDto> listofcrypto = repo.GetAllCrypto();

                //Assert
                Assert.Equal(2, listofcrypto.Count);
                Assert.Equal("bitcoin", listofcrypto[0].Name);


            }
        }

        [Fact]
        void ShouldGetAllCryptoByID()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                ICryptoPortfolioDL repo = new CryptoPortfolioDL(context);
                int cryptoid = 1;

                //Act
                CryptoDto listofcrypto = repo.GetCryptoById(cryptoid);

                //Assert
                Assert.Equal("bitcoin", listofcrypto.Name);
                Assert.Equal(100, listofcrypto.CurrentPrice);
                Assert.Equal("BTC", listofcrypto.ShortenedName);
            }
        }

        [Fact]
        void ShouldGetAllCryptoAssets()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                ICryptoPortfolioDL repo = new CryptoPortfolioDL(context);

                //Act
                List<CryptoAssetDto> listofcrypto = repo.GetAllCryptoAssets();

                //Assert
                Assert.Equal(2, listofcrypto.Count);
                Assert.Equal(100, listofcrypto[0].BuyPrice);
            }
        }

        [Fact]
        void ShouldGetAllCryptoAssetsByID()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                ICryptoPortfolioDL repo = new CryptoPortfolioDL(context);
                int userid = 1;

                //Act
                List<CryptoAssetDto> listofcrypto = repo.GetCryptoAssetsByUser(userid);

                //Assert
                Assert.Equal(2, listofcrypto.Count);
                Assert.Equal(100, listofcrypto[0].BuyPrice);
                Assert.Equal(130, listofcrypto[1].BuyPrice);
            }
        }

        /*[Fact]
        void ShouldAddCrypto()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                string name = "dodgecoin";
                decimal price = 200;
                string namee = "BTC";
                Crypto _newCrypto = new Crypto()
                {
                    Name=name,
                    CurrentPrice=price,
                    ShortenedName=namee
                };

                ICryptoPortfolioDL repo = new CryptoPortfolioDL(context);
                //Act

                repo.AddCrypto(_newCrypto);

                //Assert
                Crypto actualCrypto = context.Cryptos.First(c => c.Name == name);
                Assert.Equal(price, actualCrypto.CurrentPrice);
                Assert.Equal(namee, actualCrypto.ShortenedName);
            }
        }*/

        [Fact]
        void ShouldAddCryptoOrderHis()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
            //Arrange
                int userid = 1;
                int cryptoid = 2;
                decimal orderprice = 200;
                decimal quantity = 1;
                string ordertype = "buy";
                CryptoOrderHistory _newCrypto = new CryptoOrderHistory()
                {
                    UserId=userid,
                    CryptoId=cryptoid,
                    OrderPrice=orderprice,
                    Quantity=quantity,
                    OrderType=ordertype
                };

                ICryptoPortfolioDL repo = new CryptoPortfolioDL(context);
                //Act

                repo.AddCryptoOrderHistory(_newCrypto);

                //Assert
                CryptoOrderHistory actualCrypto = context.CryptoOrderHistories.First(c => c.CryptoId == cryptoid);
                Assert.Equal(userid, actualCrypto.UserId);
                Assert.Equal(orderprice, actualCrypto.OrderPrice);
                Assert.Equal(ordertype, actualCrypto.OrderType);
                Assert.Equal(quantity, actualCrypto.Quantity);
            }
        }

        [Fact]
        void ShouldGetAllCryptoOrderHisByID()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                ICryptoPortfolioDL repo = new CryptoPortfolioDL(context);
                int userid = 1;

                //Act
                List<CryptoOrderHistoryDto> listofcrypto = repo.GetCryptoOrderHisByUser(userid);

                //Assert
                Assert.Equal(1, listofcrypto.Count);
                Assert.Equal(100, listofcrypto[0].OrderPrice);
                Assert.Equal("Buy", listofcrypto[0].OrderType);
                Assert.Equal(1, listofcrypto[0].Quantity);
            }
        }

            

        private void Seed()
        {
            using (My2CentsContext context = new My2CentsContext(options))
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

               context.Cryptos.AddRange(
                   new Crypto
                   {
                       CryptoId = 1,
                       CurrentPrice = 100,
                       LastUpdate = System.DateTime.Now,
                       Name = "bitcoin",
                       ShortenedName = "BTC",
                       ImageURL = "string"
                   },
                    new Crypto
                   {
                       CryptoId = 2,
                       CurrentPrice = 100,
                       LastUpdate = System.DateTime.Now,
                       Name = "ethereum",
                       ShortenedName = "ETR",
                       ImageURL = "string"
                   }
                   
               ); 

               context.CryptoAssets.AddRange(
                   new CryptoAsset
                   {
                       CryptoAssetId = 1,
                       CryptoId = 1,
                       UserId = 1,
                       BuyPrice = 100,
                       BuyDate = System.DateTime.Now,
                       StopLoss = 10,
                       TakeProfit = 20,
                       Quantity = 1
                   },
                   new CryptoAsset
                   {
                       CryptoAssetId = 2,
                       CryptoId = 1,
                       UserId = 1,
                       BuyPrice = 130,
                       BuyDate = System.DateTime.Now,
                       StopLoss = 10,
                       TakeProfit = 20,
                       Quantity = 2
                   }
               );

               context.CryptoOrderHistories.AddRange(
                   new CryptoOrderHistory
                   {
                       CryptoOrderId = 1,
                       UserId = 1,
                       CryptoId = 1,
                       OrderPrice = 100,
                       Quantity = 1,
                       OrderType = "Buy",
                       OrderTime = System.DateTime.Now
                   }
               );
               context.SaveChanges();
            }
        }
    }
}
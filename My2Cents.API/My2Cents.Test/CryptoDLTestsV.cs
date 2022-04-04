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

       /* [Fact]
        void ShouldGetAllCryptoAssets()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {

            }
        }*/

        [Fact]
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
        }

        private void Seed()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
               context.Database.EnsureDeleted();
               context.Database.EnsureCreated();

               context.Cryptos.AddRange(
                   new Crypto
                   {
                       CryptoId = 1,
                       CurrentPrice = 100,
                       LastUpdate = System.DateTime.Now,
                       Name = "bitcoin",
                       ShortenedName = "BTC"
                   },
                    new Crypto
                   {
                       CryptoId = 2,
                       CurrentPrice = 100,
                       LastUpdate = System.DateTime.Now,
                       Name = "ethereum",
                       ShortenedName = "ETR"
                   }
                   
               ); 

             /*  context.CryptoAssets.AddRange(
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
               );*/
               context.SaveChanges();
            }
        }
    }
}
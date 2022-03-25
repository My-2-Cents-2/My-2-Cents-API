using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DataInfrastructure;
using Xunit;
namespace My2Cents.Test
{
    public class CryptoDLTestsV
    {
        private readonly DbContextOptions<My2CentsContext> options;

        public CryptoDLTestsV()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = Test.db").Options;
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
                List<Crypto> listofcrypto = repo.GetAllCrypto();

                //Assert
                Assert.Equal(2, listofcrypto.Count);


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
               context.SaveChanges();
            }
        }
    }
}
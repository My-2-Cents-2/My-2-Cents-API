using Microsoft.EntityFrameworkCore;
using Moq;
using My2Cents.DatabaseManagement;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace My2Cents.Test
{
    public class InvenstmentPlatformDLTest
    {
        private readonly DbContextOptions<My2CentsContext> options;

        public InvenstmentPlatformDLTest()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = Test.db").Options;
            Seed();
        }

        [Fact]
        async Task ShouldUpdateCrypto()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IInvesmenentPlatformManagementDL repo = new InvesmenentPlatformManagementDL(context);
        
                //Act
                List<CryptoDto> listOfCrypto = await repo.UpdateCryptosData();
        
                //Assert
                Assert.Equal(52, listOfCrypto.Count);
            }
        }


        private void Seed()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                context.Database.EnsureDeletedAsync();
                context.Database.EnsureCreatedAsync();

                context.Cryptos.AddRange(
                    new Crypto{
                        CryptoId = 1,
                        CurrentPrice = 1,
                        LastUpdate = DateTime.UtcNow,
                        ImageURL = "testingURL1",
                        PriceChange = 1,
                        PriceChangePercentage = 1,
                        Name = "Crypto1",
                        ShortenedName = "Cry1",
                        CryptoNameId = "CryptoNameID1"
                    },
                    new Crypto{
                        CryptoId = 2,
                        CurrentPrice = 2,
                        LastUpdate = DateTime.UtcNow,
                        ImageURL = "testingURL2",
                        PriceChange = 22,
                        PriceChangePercentage = 2222,
                        Name = "Crypto2",
                        ShortenedName = "Cry2",
                        CryptoNameId = "CryptoNameID2"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
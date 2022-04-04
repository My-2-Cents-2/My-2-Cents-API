using Microsoft.EntityFrameworkCore;
using Moq;
using My2Cents.DatabaseManagement;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace My2Cents.Test
{
    public class InvenstmentPlatformTest
    {
        private readonly DbContextOptions<My2CentsContext> options;

        public InvenstmentPlatformTest()
        {
            options = new DbContextOptionsBuilder<My2CentsContext>().UseSqlite("Filename = InvestmentPlatformTest.db").Options;
            Seed();
        }

        private void Seed()
        {
            throw new NotImplementedException();
        }

        [Fact]
        void ShouldUpdateCrypto()
        {
            using (My2CentsContext context = new My2CentsContext(options))
            {
                //Arrange
                IInvesmenentPlatformManagementDL repo = new InvesmenentPlatformManagementDL(context);
        
                //Act
                //List<CryptoDto> listOfCrypto = repo.UpdateCryptosData();
        
                //Assert
            }
           
        }
    }
}
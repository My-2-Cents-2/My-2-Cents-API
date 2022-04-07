using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using My2Cents.DataInfrastructure.Models;
using My2Cents.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using My2Cents.DatabaseManagement;

namespace My2Cents.Test
{
    public class EFRepositoryTests
    {
        Mock<IRepository> _repositoryMock = new();


        [Fact]
        public async Task GetTransactions_ShouldReturnTransaction_WhenItExists()
        {

            //Arrange
            var accountId = 2;

            var transactionDto = new List<TransactionDto>();
            TransactionDto T = new TransactionDto();
            transactionDto.Add(new TransactionDto());
            transactionDto[0].TransactionId = 1;
            transactionDto[0].AccountId = 1;
            transactionDto[0].Amount = 306;
            transactionDto[0].TransactionDate = DateTime.Now;
            transactionDto[0].Authorized = "Yes";
            transactionDto[0].LineAmount = 10010;
            transactionDto[0].AccountType = "Checking";
            transactionDto[0].TotalBalance = 10010;

            //transactionDto.Add(new(1, accountId, 306, "Bagels", DateTime.Now, "Yes", 306, "Checking", 10010));






            _repositoryMock.Setup(x => x.GetTransactions(accountId)).Returns(Task.FromResult((IEnumerable<TransactionDto>)transactionDto));

            //Act
            var actual = await _repositoryMock.Object.GetTransactions(accountId);
            var expected = transactionDto;

            //Assert
            Assert.Equal(expected, actual);


        }



        [Fact]
        public async Task GetTransactionsTestBadRequestController()
        {
            var accountId = 2;
            var transactionDto = new List<TransactionDto>();

            _repositoryMock.Setup(r => r.GetTransactions(accountId)).ReturnsAsync(transactionDto);

            var controller = new TransactionsController(_repositoryMock.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();


            //Act
            ActionResult<IEnumerable<TransactionDto>> result = await controller.GetTransactions(accountId);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Account not found.", badRequestResult.Value);

        }


        [Fact]
        public async Task GetTransactionsTestController()
        {
            var accountId = 2;
            var transactionDto = new List<TransactionDto>();

            TransactionDto T = new TransactionDto();
            transactionDto.Add(new TransactionDto());
            transactionDto[0].AccountId = accountId;
            transactionDto[0].Amount = 306;
            transactionDto[0].TransactionDate = DateTime.Now;
            transactionDto[0].Authorized = "Yes";
            transactionDto[0].LineAmount = 10010;
            transactionDto[0].AccountType = "Checking";
            transactionDto[0].TotalBalance = 10010;


            transactionDto.Add(new TransactionDto());
            transactionDto[1].AccountId = accountId;
            transactionDto[1].Amount = 406;
            transactionDto[1].TransactionDate = DateTime.Now;
            transactionDto[1].Authorized = "Yes";
            transactionDto[1].LineAmount = 20010;
            transactionDto[1].AccountType = "Checking";
            transactionDto[1].TotalBalance = 20010;

            _repositoryMock.Setup(r => r.GetTransactions(accountId)).ReturnsAsync(transactionDto);

            var controller = new TransactionsController(_repositoryMock.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();


            //Act
            try
            {
                ActionResult<IEnumerable<TransactionDto>> result = await controller.GetTransactions(accountId);
                //Assert
                OkObjectResult objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
                IEnumerable<TransactionDto> body = Assert.IsAssignableFrom<IEnumerable<TransactionDto>>(objectResult.Value);
                List<TransactionDto> list = body.ToList();
                Assert.Equal(expected: transactionDto, actual: list);


            }
            catch (Exception ex)
            {
                // Console.Error.WriteLine(ex);
            }



        }

    }
}

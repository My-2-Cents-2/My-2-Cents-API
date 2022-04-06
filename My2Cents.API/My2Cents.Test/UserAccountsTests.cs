using Microsoft.AspNetCore.Mvc;
using Moq;
using My2Cents.API.Controllers;
using My2Cents.DatabaseManagement;
using My2Cents.DataInfrastructure;
using My2Cents.DataInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace My2Cents.Test
{
    public class UserAccountsTests
    {
        [Theory]
        [InlineData(1)]
        public async Task ValidGetUserAccounts(int userId)
        {
            // arrange
            ActionResult<IEnumerable<AccountListDto>> accountListDto = new AccountListDto[]
            {
                new AccountListDto()
                {
                    AccountID = 1,
                    TotalBalance = 500,
                    AccountType = "Savings",
                    Interest = 5
                }
            };

            Mock<IRepository> _repository = new Mock<IRepository>();
            _repository.Setup(u => u.GetUserAccounts(userId)).ReturnsAsync(accountListDto);
            AccountTypeController accountTypeController = new(_repository.Object);

            // act
            var result = await accountTypeController.GetUserAccounts(userId);

            // assert
            Assert.Equal(accountListDto, result);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task ValidUserAccountPost(
            int userId,
            int accountTypeId)
        {
            // Arrange
            AccountDto accountDto = new()
            {
                AccountId = 1,
                UserId = userId,
                TotalBalance = 100,
                AccountTypeId = accountTypeId,
                Interest = 1
            };

            ActionResult<IEnumerable<AccountTypeDto>> accountListDto = new AccountTypeDto[]
            {
                new AccountTypeDto()
                {
                    AccountTypeId = 1,
                    AccountType1 = "Checking"
                },
                new AccountTypeDto()
                {
                    AccountTypeId = 2,
                    AccountType1 = "Savings"
                }
            };

            Mock<IRepository> _repository = new Mock<IRepository>();
            _repository.Setup(v => v.GetAccountTypes()).ReturnsAsync(accountListDto);
            _repository.Setup(u => u.PostUserAccount(
                userId,
                accountTypeId,
                accountListDto)).ReturnsAsync(1);
            AccountTypeController accountTypeController = new(_repository.Object);

            // act
            var result = await accountTypeController.PostUserAccount(accountDto);

            // assert
            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(1, -1)]
        public async Task InvalidUserAccountPost(
            int userId,
            int accountTypeId)
        {
            // arrange
            AccountDto accountDto = new()
            {
                UserId = userId,
                AccountTypeId = accountTypeId
            };

            ActionResult<IEnumerable<AccountTypeDto>> accountListDto = new AccountTypeDto[]
            {
                new AccountTypeDto()
                {
                    AccountTypeId = 1,
                    AccountType1 = "Checking"
                },
                new AccountTypeDto()
                {
                    AccountTypeId = 2,
                    AccountType1 = "Savings"
                }
            };

            Mock<IRepository> _repository = new Mock<IRepository>();
            _repository.Setup(v => v.GetAccountTypes()).ReturnsAsync(accountListDto);
            _repository.Setup(u => u.PostUserAccount(
                userId,
                accountTypeId,
                accountListDto)).ReturnsAsync(400);
            AccountTypeController accountTypeController = new(_repository.Object);

            // act
            var result = await accountTypeController.PostUserAccount(accountDto);

            // assert
            Assert.Equal(400, result);
        }
    }
}

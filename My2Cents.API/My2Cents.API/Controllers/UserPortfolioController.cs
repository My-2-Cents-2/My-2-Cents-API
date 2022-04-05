using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using My2Cents.DatabaseManagement.Interfaces;
using My2Cents.DatabaseManagement.Implements;
using My2Cents.Logic.Implements;
using My2Cents.Logic.Interfaces;
using My2Cents.DataInfrastructure;
using My2Cents.API.Consts;
using My2Cents.API.DataTransferObjects;
using My2Cents.DataInfrastructure.Models;

namespace My2Cents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserPortfolioController : ControllerBase
    {
        private readonly ICryptoPortfolioBL _cryptoBL;
        private readonly IStockPortfolioManagementBL _stockPortfolioBL;
        public UserPortfolioController(ICryptoPortfolioBL c_cryptoPortfolioBL, IStockPortfolioManagementBL s_stockPortfolioBL)
        {
            _cryptoBL = c_cryptoPortfolioBL;
            _stockPortfolioBL = s_stockPortfolioBL;
        }

        // GET: api/UserPortfolio
        [HttpGet(RouteConfigs.UserInvestmentSum)]
        public async Task<IActionResult> GetUserInvestmentSum(int userId)
        {
            try
            {
                decimal _result = (await _cryptoBL.GetUserCryptoInvestmentSum(userId)) + (await _stockPortfolioBL.GetUserStockInvestmentSum(userId));
                UserInvestmentInfoForm _userInvestmentResult = new UserInvestmentInfoForm()
                {
                    UserInvestmentSum = _result
                };
                Log.Information("Route: " + RouteConfigs.UserInvestmentSum);
                Log.Information("Get User Investment Sum");

                return Ok(_userInvestmentResult);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserInvestmentSum);
                Log.Warning(e.Message);
                return NotFound(e.Message);
            }
        }

        
    }
}

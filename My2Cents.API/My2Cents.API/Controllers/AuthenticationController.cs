using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My2Cents.API.AuthenticationService.Interfaces;
using My2Cents.API.DataTransferObjects;
using My2Cents.DataInfrastructure;

namespace My2Cents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAccessTokenManager _accessTokenManager;
        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        RoleManager<ApplicationRole> roleManager,
                                        IAccessTokenManager accessTokenManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _accessTokenManager = accessTokenManager;
        }

        // POST: api/Authentication/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterForm registerFrom)
        {
            if (!(await _roleManager.RoleExistsAsync("User")))
            {
                await _roleManager.CreateAsync(new ApplicationRole() { Name = "User" });
            }

            var newId = Guid.NewGuid().ToString();
            ApplicationUser _identity = new ApplicationUser()
            {
                UserName = registerFrom.Username,
                Email = registerFrom.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(_identity, registerFrom.Password);

            if (result.Succeeded)
            {
                var userFromDB = await _userManager.FindByNameAsync(_identity.UserName);

                // Add default role to user("User")
                await _userManager.AddToRoleAsync(userFromDB, "User");

                var roles = await _userManager.GetRolesAsync(userFromDB);
                return Ok(new
                {
                    Result = result,
                    UserId = userFromDB.Id,
                    Username = userFromDB.UserName,
                    Email = userFromDB.Email,
                    Token = _accessTokenManager.GenerateToken(userFromDB, roles)
                });
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                }

                return BadRequest(new { Result = $"Register Fail: {stringBuilder.ToString()}" });
            }
        }

        // POST: api/Authentication/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginForm loginForm)
        {
            var userFromDB = await _userManager.FindByNameAsync(loginForm.Username);

            if (userFromDB == null)
            {
                return BadRequest(new { Result = "Login Failed! User didn't exist in the database!" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userFromDB, loginForm.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest(new { Result = "Login Failed! Password didn't matched in the database!" });
            }
            var roles = await _userManager.GetRolesAsync(userFromDB);

            return Ok(new
            {
                Result = result,
                UserId = userFromDB.Id,
                Username = userFromDB.UserName,
                Email = userFromDB.Email,
                Token = _accessTokenManager.GenerateToken(userFromDB, roles)
            });
        }
    }
}

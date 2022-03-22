using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using My2Cents.API.AuthenticationService.Interfaces;
using My2Cents.DataInfrastructure;

namespace My2Cents.API.AuthenticationService.Implements
{
    public class AccessTokenManager : IAccessTokenManager
    {
        private readonly IDistributedCache accessTokensCache;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration _config;
        public AccessTokenManager(IConfiguration config, IDistributedCache distributedCache, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            accessTokensCache = distributedCache;
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = Encoding.ASCII.GetBytes(_config["Token:Key"]);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken accessToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(accessToken);
        }

        public async Task<bool> IsActiveAsync(string token)
        {
            return await accessTokensCache.GetStringAsync(GetKey(token)) == null;
        }

        public async Task<bool> IsCurrentActiveToken() => await IsActiveAsync(GetCurrentAsync());

        private string GetCurrentAsync()
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token) => $"tokens:{token}:deactivated";
    }
}
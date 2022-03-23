using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using My2Cents.DataInfrastructure;

namespace My2Cents.API.AuthenticationService.Interfaces
{
    public interface IAccessTokenManager
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
        Task<bool> IsCurrentActiveToken();
        Task<bool> IsActiveAsync(string token);
    }
}
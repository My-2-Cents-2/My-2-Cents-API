using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using My2Cents.DataInfrastructure;

namespace My2Cents.API.AuthenticationService.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task Execute(string apiKey, string subject, string message, string toEmail);

    }
}
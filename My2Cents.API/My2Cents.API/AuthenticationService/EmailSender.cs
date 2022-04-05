using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using My2Cents.API.AuthenticationService.Interfaces;
using My2Cents.DataInfrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace My2Cents.API.AuthenticationService.Implements
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly IConfiguration configuration;
        public EmailSender(IConfiguration configuration,
                            ILogger<EmailSender> logger)
        {
            _logger = logger;
            this.configuration = configuration;
        }
        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var senderEmailAddress = configuration["SendGrid:EmailAddress"];
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmailAddress, "My2Cents"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                    ? $"Email to {toEmail} queued successfully!"
                                    : $"Failure Email to {toEmail}");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var sendGridApiKey = configuration["SendGrid:My2CentsSendGridEmailAPI"];
            if (string.IsNullOrEmpty(sendGridApiKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(sendGridApiKey, subject, message, toEmail);
        }
    }
}
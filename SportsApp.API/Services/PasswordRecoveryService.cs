using SportsApp.API.Models;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SportsApp.API.Services
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly IConfiguration _config;

        public PasswordRecoveryService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendRecoveryEmailAsync(ApplicationUser user, string token)
        {
            try
            {
                var smtpServer = _config["SmtpSettings:Server"];
                var smtpPort = int.Parse(_config["SmtpSettings:Port"]);
                var senderEmail = _config["SmtpSettings:SenderEmail"];
                var senderName = _config["SmtpSettings:SenderName"];
                var smtpUser = _config["SmtpSettings:Username"];
                var smtpPass = _config["SmtpSettings:Password"];

                var frontendBaseUrl = _config["Frontend:BaseUrl"];
                var resetLink = $"{frontendBaseUrl}/reset-password?email={WebUtility.UrlEncode(user.Email)}&token={WebUtility.UrlEncode(token)}";

                if (string.IsNullOrEmpty(user.Email))
                {
                    throw new ArgumentException("User email cannot be null or empty.", nameof(user.Email));
                }

                var message = new MailMessage
                {
                    From = new MailAddress(senderEmail, senderName),
                    Subject = "Password Reset",
                    Body = $"Click the link to reset your password:\n\n{resetLink}",
                    IsBodyHtml = false
                };

                message.To.Add(user.Email);

                using var smtp = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };

                // ToDo: Bypass SSL certificate validation (not recommended for production)
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}

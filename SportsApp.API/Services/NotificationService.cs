using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SportsApp.API.Services
{
    

    public class NotificationService : INotificationService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public NotificationService(IConfiguration configuration)
        {
            _smtpServer = configuration["SMTP:Server"];
            _smtpPort = int.Parse(configuration["SMTP:Port"]);
            _smtpUser = configuration["SMTP:Username"];
            _smtpPass = configuration["SMTP:Password"];
            _senderEmail = configuration["SMTP:SenderEmail"];
            _senderName = configuration["SMTP:SenderName"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true // Important for security
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail, _senderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true // You can send HTML content if needed
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

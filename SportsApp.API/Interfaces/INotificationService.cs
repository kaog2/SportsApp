using SportsApp.API.Models;

public interface INotificationService
{
    Task SendEmailAsync(string toEmail, string subject, string message);
}

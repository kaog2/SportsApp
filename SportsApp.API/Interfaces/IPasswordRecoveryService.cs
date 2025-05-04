using SportsApp.API.Models;

public interface IPasswordRecoveryService
{
    Task SendRecoveryEmailAsync(ApplicationUser user, string token);
}
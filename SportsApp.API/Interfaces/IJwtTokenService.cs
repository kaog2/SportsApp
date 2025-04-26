using SportsApp.API.Models;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user);
}
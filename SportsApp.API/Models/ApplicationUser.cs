using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace SportsApp.API.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Full name of the user
        public string? FullName { get; set; }

        // Player level or experience
        public int Level { get; set; }

        // Short biography
        public string? Bio { get; set; }

        // Tags related to the user (like skills, preferences)
        public List<UserTag>? Tags { get; set; }

        // Matches the user has participated in
        public List<Match>? Matches { get; set; }

        // Role of the user (Admin, Player, Kinesiologist)
        public string? Role { get; set; }

        // Specialty for Kinesiologist (optional)
        public string? Specialty { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace SportsApp.API.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public int Level { get; set; }
        public string Bio { get; set; }
        public List<UserTag> Tags { get; set; }
        public List<Match> Matches { get; set; }
    }
}

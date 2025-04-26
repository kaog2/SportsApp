namespace SportsApp.API.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public Guid FacilityId { get; set; }
        public Facility? Facility { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<ApplicationUser>? Players { get; set; }
        public int MaxPlayers { get; set; } = 4;
        public bool IsPublic { get; set; }
    }
}

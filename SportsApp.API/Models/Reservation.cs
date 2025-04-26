namespace SportsApp.API.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MatchId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

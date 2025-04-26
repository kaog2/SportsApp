using SportsApp.API.Enum;

namespace SportsApp.API.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid CourtId { get; set; }
        public Court? Court { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // New: Status of the reservation
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;

        // Optional: Reason for cancellation or modification
        public string? Notes { get; set; }
    }
}


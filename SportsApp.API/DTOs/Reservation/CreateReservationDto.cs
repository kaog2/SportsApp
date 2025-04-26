namespace SportsApp.API.DTOs.Reservation
{
    public class CreateReservationDto
    {
        public Guid CourtId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

namespace SportsApp.API.DTOs.Reservation
{
    public class UpdateReservationDto
    {
        public DateTime NewStartTime { get; set; }
        public DateTime NewEndTime { get; set; }
        public string? Reason { get; set; }
    }
}
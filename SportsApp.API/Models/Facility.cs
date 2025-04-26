namespace SportsApp.API.Models
{
    public abstract class Facility
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Surface { get; set; }
        public decimal PricePerHour { get; set; }
        public Guid FacilityTypeId { get; set; }
        public FacilityType? FacilityType { get; set; }
    }
}

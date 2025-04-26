namespace SportsApp.API.DTOs
{
    public class RegisterDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Level { get; set; }
        public string? Bio { get; set; }

        // Specialty is optional, used when registering a Kinesiologist
        public string? Specialty { get; set; }
    }
}

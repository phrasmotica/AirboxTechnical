namespace AirboxTechnical.Models
{
    public class CreateUserLocationRequest
    {
        public User User { get; set; } = default!;

        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

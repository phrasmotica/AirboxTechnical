namespace AirboxTechnical.Data.Models
{
    public class UserLocation
    {
        public string Id { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

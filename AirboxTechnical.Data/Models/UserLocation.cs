namespace AirboxTechnical.Data.Models
{
    public class UserLocation
    {
        // TODO: add GUID field

        public User User { get; set; } = default!;

        public DateTime Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

using AirboxTechnical.Validation;

namespace AirboxTechnical.Models
{
    public class CreateUserLocationRequest
    {
        public User User { get; set; } = default!;

        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Represents the user's latitude in decimal degrees.
        /// Value should be in the range (-90, 90), where -90 is 90°S and 90 is 90°N.
        /// See <see cref="CreateUserLocationRequestValidator" />.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Represents the user's longitude in decimal degrees.
        /// Should be in the range (-180, 180], where -180 is 180°W and 180 is 180°E.
        /// See <see cref="CreateUserLocationRequestValidator" />.
        /// </summary>
        public double Longitude { get; set; }
    }
}

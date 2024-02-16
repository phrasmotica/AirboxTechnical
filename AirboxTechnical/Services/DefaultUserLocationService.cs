using AirboxTechnical.Models;

namespace AirboxTechnical.Services
{
    public class DefaultUserLocationService : IUserLocationService
    {
        private readonly List<UserLocation> _locations = [];

        public Task<UserLocation> AddLocation(UserLocation location)
        {
            _locations.Add(location);

            return Task.FromResult(location);
        }

        public Task<UserLocation?> GetLastLocation(string userId)
        {
            var location = FindLastLocation(userId);

            return Task.FromResult(location);
        }

        public Task<IEnumerable<UserLocation>> GetLastLocations()
        {
            var lastLocations = ListUsers().Select(u => FindLastLocation(u.Id)!);

            return Task.FromResult(lastLocations);
        }

        public Task<IEnumerable<UserLocation>> GetLocations(string userId)
        {
            var locations = FindLocations(userId);

            return Task.FromResult(locations);
        }

        private IEnumerable<User> ListUsers()
        {
            return _locations.Select(l => l.User).DistinctBy(u => u.Id);
        }

        private IEnumerable<UserLocation> FindLocations(string userId)
        {
            return _locations.Where(l => string.Equals(l.User.Id, userId));
        }

        private UserLocation? FindLastLocation(string userId)
        {
            var locations = FindLocations(userId);
            if (!locations.Any())
            {
                return null;
            }

            return locations
                .Aggregate((a, b) => a.Timestamp > b.Timestamp ? a : b);
        }
    }
}

using AirboxTechnical.Models;

namespace AirboxTechnical.Services
{
    public class DefaultUserLocationService : IUserLocationService
    {
        private readonly List<UserLocation> _locations = [];

        private readonly IUserService _userService;

        public DefaultUserLocationService(IUserService userService) 
        {
            _userService = userService;
        }

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

        public async Task<IEnumerable<UserLocation>> GetLastLocations()
        {
            var users = await _userService.ListUsers();

            var lastLocations = users
                .Select(u => FindLastLocation(u.Id))
                .Where(l => l is not null);

            return lastLocations!;
        }

        public Task<IEnumerable<UserLocation>> GetLocations(string userId)
        {
            var locations = FindLocations(userId);

            return Task.FromResult(locations);
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

            return locations.Aggregate((a, b) => a.Timestamp > b.Timestamp ? a : b);
        }
    }
}

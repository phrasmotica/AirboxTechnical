using AirboxTechnical.Data.Models;

namespace AirboxTechnical.Data.Services
{
    /// <summary>
    /// In-memory implementation of a data service for storing user location records.
    /// </summary>
    public class DefaultUserLocationService : IUserLocationService
    {
        private readonly List<UserLocation> _locations = [];

        private readonly IUserService _userService;

        public DefaultUserLocationService(IUserService userService) 
        {
            _userService = userService;
        }

        public async Task<UserLocation> AddLocation(UserLocation location)
        {
            var user = await _userService.GetUser(location.User.Id);
            if (user is null)
            {
                throw new InvalidOperationException($"User {location.User.Id} does not exist!");
            }

            // TODO: prevent a new location with the same timestamp as an existing location
            // for the given user from being added

            _locations.Add(location);

            return location;
        }

        public async Task<UserLocation?> GetLastLocation(string userId)
        {
            var user = await _userService.GetUser(userId);
            if (user is null)
            {
                return null;
            }

            return FindLastLocation(userId);
        }

        public async Task<IEnumerable<UserLocation>> GetLastLocations()
        {
            var users = await _userService.ListUsers();
            if (!users.Any())
            {
                return [];
            }

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

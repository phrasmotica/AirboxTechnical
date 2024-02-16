using AirboxTechnical.Models;

namespace AirboxTechnical.Services
{
    public class DefaultUserLocationService : IUserLocationService
    {
        public Task<UserLocation> AddLocation(UserLocation location)
        {
            throw new NotImplementedException();
        }

        public Task<UserLocation> GetLastLocation(string userId)
        {
            return Task.FromResult(new UserLocation
            {
                User = new User
                {
                    Id = userId,
                },
            });
        }

        public Task<IEnumerable<UserLocation>> GetLastLocations()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserLocation>> GetLocations(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

using AirboxTechnical.Models;
using AirboxTechnical.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirboxTechnical.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLocationController : ControllerBase
    {
        private readonly IUserLocationService _userLocationService;
        private readonly ILogger<UserLocationController> _logger;

        public UserLocationController(
            IUserLocationService userLocationService,
            ILogger<UserLocationController> logger)
        {
            _userLocationService = userLocationService;
            _logger = logger;
        }

        [HttpPost(Name = "AddLocation")]
        public async Task<UserLocation> AddLocation(UserLocation location)
        {
            _logger.LogInformation($"Adding location for user {location.User.Id}");

            return await _userLocationService.AddLocation(location);
        }

        [HttpGet("recent/{userId}", Name = "GetLastLocation")]
        public async Task<UserLocation> GetLastLocation(string userId)
        {
            _logger.LogInformation($"Getting last location for user {userId}");

            return await _userLocationService.GetLastLocation(userId);
        }

        [HttpGet("recent", Name = "GetLastLocations")]
        public async Task<IEnumerable<UserLocation>> GetLastLocations()
        {
            _logger.LogInformation("Getting last locations for all users");

            return await _userLocationService.GetLastLocations();
        }

        [HttpGet("history/{userId}", Name = "GetLocations")]
        public async Task<IEnumerable<UserLocation>> GetLocations(string userId)
        {
            _logger.LogInformation($"Getting all locations for user {userId}");

            return await _userLocationService.GetLocations(userId);
        }
    }
}

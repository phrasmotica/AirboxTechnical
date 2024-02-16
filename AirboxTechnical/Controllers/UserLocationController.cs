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
        public async Task<ActionResult<UserLocation>> AddLocation(UserLocation location)
        {
            // TODO: validate UserLocation model

            _logger.LogInformation($"Adding location for user {location.User.Id}");

            var newLocation = await _userLocationService.AddLocation(location);

            return Ok(newLocation);
        }

        [HttpGet("recent/{userId}", Name = "GetLastLocation")]
        public async Task<ActionResult<UserLocation>> GetLastLocation(string userId)
        {
            _logger.LogInformation($"Getting last location for user {userId}");

            var location = await _userLocationService.GetLastLocation(userId);
            if (location is null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpGet("recent", Name = "GetLastLocations")]
        public async Task<ActionResult<IEnumerable<UserLocation>>> GetLastLocations()
        {
            _logger.LogInformation("Getting last locations for all users");

            var locations = await _userLocationService.GetLastLocations();

            return Ok(locations);
        }

        [HttpGet("history/{userId}", Name = "GetLocations")]
        public async Task<ActionResult<IEnumerable<UserLocation>>> GetLocations(string userId)
        {
            _logger.LogInformation($"Getting all locations for user {userId}");

            var locations = await _userLocationService.GetLocations(userId);

            return Ok(locations);
        }
    }
}

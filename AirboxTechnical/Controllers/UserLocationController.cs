using AirboxTechnical.Data.Models;
using AirboxTechnical.Data.Services;
using AirboxTechnical.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AirboxTechnical.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLocationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserLocationService _userLocationService;
        private readonly IValidator<CreateUserLocationRequest> _createRequestValidator;
        private readonly ILogger<UserLocationController> _logger;

        // TODO: implement Polly resilience policy, for wrapping calls to data services

        public UserLocationController(
            IUserService userService,
            IUserLocationService userLocationService,
            IValidator<CreateUserLocationRequest> createRequestValidator,
            ILogger<UserLocationController> logger)
        {
            _userService = userService;
            _userLocationService = userLocationService;
            _createRequestValidator = createRequestValidator;
            _logger = logger;
        }

        [HttpPost(Name = "AddLocation")]
        public async Task<ActionResult<UserLocation>> AddLocation(CreateUserLocationRequest request)
        {
            var validation = _createRequestValidator.Validate(request);
            if (!validation.IsValid)
            {
                var errors = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                _logger.LogError($"Request to create user location was invalid: {errors}");

                return BadRequest();
            }

            var userId = request.UserId;

            var user = await _userService.GetUser(userId);
            if (user is null)
            {
                _logger.LogError($"User {userId} does not exist!");

                // TODO: figure out a better status code for this
                return StatusCode(503);
            }

            _logger.LogInformation($"Adding location for user {userId}");

            // TODO: use filter attributes for error handling
            try
            {
                // TODO: use AutoMapper for translation of DTOs
                var newLocation = await _userLocationService.AddLocation(new()
                {
                    UserId = user.Id,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Timestamp = request.Timestamp,
                });

                // TODO: raise an event indicating that a user location has been created

                return Ok(newLocation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create user location! Exception: {ex.Message}");

                return StatusCode(503);
            }
        }

        [HttpGet("recent/{userId}", Name = "GetLastLocation")]
        public async Task<ActionResult<UserLocation>> GetLastLocation(string userId)
        {
            var user = await _userService.GetUser(userId);
            if (user is null)
            {
                _logger.LogError($"User {userId} does not exist!");

                return NotFound();
            }

            _logger.LogInformation($"Getting last location for user {userId}");

            var location = await _userLocationService.GetLastLocation(userId);
            if (location is null)
            {
                _logger.LogError($"No locations exist for user {userId}!");

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
            var user = await _userService.GetUser(userId);
            if (user is null)
            {
                _logger.LogError($"User {userId} does not exist!");

                return NotFound();
            }

            _logger.LogInformation($"Getting all locations for user {userId}");

            var locations = await _userLocationService.GetLocations(userId);

            return Ok(locations);
        }
    }
}

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

        [HttpGet(Name = "GetLastLocation")]
        public async Task<UserLocation> GetLastLocation(string userId)
        {
            return await _userLocationService.GetLastLocation(userId);
        }
    }
}

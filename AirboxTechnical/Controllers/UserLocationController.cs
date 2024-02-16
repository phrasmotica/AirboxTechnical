using AirboxTechnical.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirboxTechnical.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLocationController : ControllerBase
    {
        private readonly ILogger<UserLocationController> _logger;

        public UserLocationController(ILogger<UserLocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetLastLocation")]
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
    }
}

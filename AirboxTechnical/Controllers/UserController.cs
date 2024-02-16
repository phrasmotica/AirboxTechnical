using AirboxTechnical.Data.Services;
using AirboxTechnical.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AirboxTechnical.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateUserRequest> _createRequestValidator;
        private readonly ILogger<UserController> _logger;

        // TODO: implement Polly resilience policy, for wrapping calls to data services

        public UserController(
            IUserService userService,
            IValidator<CreateUserRequest> createRequestValidator,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _createRequestValidator = createRequestValidator;
            _logger = logger;
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<User>> CreateUser(CreateUserRequest request)
        {
            var validation = _createRequestValidator.Validate(request);
            if (!validation.IsValid)
            {
                var errors = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                _logger.LogError($"Request to create user was invalid: {errors}");

                return BadRequest();
            }

            _logger.LogInformation($"Creating user with name {request.Name}");

            // TODO: use filter attributes for error handling
            try
            {
                // TODO: use AutoMapper for translation of DTOs
                var newUser = await _userService.AddUser(new()
                {
                    Name = request.Name,
                });

                // TODO: raise an event indicating that a user has been created

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create user! Exception: {ex.Message}");

                return StatusCode(503);
            }
        }
    }
}

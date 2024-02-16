using AirboxTechnical.Models;

namespace AirboxTechnical.Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);

        Task<User?> GetUser(string id);
    }
}

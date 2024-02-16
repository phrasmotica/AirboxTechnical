using AirboxTechnical.Data.Models;

namespace AirboxTechnical.Data.Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);

        Task<User?> GetUser(string id);

        Task<List<User>> ListUsers();
    }
}

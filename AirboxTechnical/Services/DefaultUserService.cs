using AirboxTechnical.Models;

namespace AirboxTechnical.Services
{
    public class DefaultUserService : IUserService
    {
        private readonly List<User> _users = [];

        public Task<User> AddUser(User user)
        {
            _users.Add(user);

            return Task.FromResult(user);
        }

        public Task<User?> GetUser(string id)
        {
            return Task.FromResult(_users.SingleOrDefault(u => string.Equals(u.Id, id)));
        }
    }
}

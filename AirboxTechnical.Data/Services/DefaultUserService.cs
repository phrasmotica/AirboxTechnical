﻿using AirboxTechnical.Data.Models;

namespace AirboxTechnical.Data.Services
{
    /// <summary>
    /// In-memory implementation of a data service for storing user location records.
    /// </summary>
    public class DefaultUserService : IUserService
    {
        private readonly List<User> _users = [];

        public Task<User> AddUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();

            _users.Add(user);

            return Task.FromResult(user);
        }

        public Task<User?> GetUser(string id)
        {
            return Task.FromResult(_users.SingleOrDefault(u => string.Equals(u.Id, id)));
        }

        public Task<List<User>> ListUsers()
        {
            return Task.FromResult(_users);
        }
    }
}

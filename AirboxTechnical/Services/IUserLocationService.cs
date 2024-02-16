﻿using AirboxTechnical.Models;

namespace AirboxTechnical.Services
{
    public interface IUserLocationService
    {
        Task<UserLocation> AddLocation(UserLocation location);

        Task<UserLocation?> GetLastLocation(string userId);

        Task<IEnumerable<UserLocation>> GetLastLocations();

        Task<IEnumerable<UserLocation>> GetLocations(string userId);
    }
}

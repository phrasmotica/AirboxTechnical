using AirboxTechnical.Data.Models;
using AirboxTechnical.Data.Services;
using Moq;

namespace AirboxTechnical.Data.Tests
{
    public class DefaultUserLocationServiceTests
    {
        [Test]
        public async Task AddLocation_SuccessfullyAddsLocation()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            userService.Setup(m => m.GetUser(It.IsAny<string>())).ReturnsAsync(new User());

            var service = new DefaultUserLocationService(userService.Object);

            // Act
            await service.AddLocation(new()
            {
                Latitude = 1,
                Longitude = 1,
                Timestamp = DateTime.Now,
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            // Assert
            var location = await service.GetLastLocation("user1");

            Assert.That(location, Is.Not.Null);
            Assert.That(location.Latitude, Is.EqualTo(1));
            Assert.That(location.Longitude, Is.EqualTo(1));
        }

        [Test]
        public async Task GetLastLocation_NoLocationForUser_ReturnsNull()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            var service = new DefaultUserLocationService(userService.Object);

            // Act
            var location = await service.GetLastLocation("user1");

            // Assert
            Assert.That(location, Is.Null);
        }

        [Test]
        public async Task GetLastLocation_ReturnsMostRecentLocation()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            userService.Setup(m => m.GetUser(It.IsAny<string>())).ReturnsAsync(new User());

            var service = new DefaultUserLocationService(userService.Object);

            var date = new DateTime(2024, 2, 16, 10, 58, 0);

            // Act
            await service.AddLocation(new()
            {
                Latitude = 10,
                Longitude = 10,
                Timestamp = date,
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            await service.AddLocation(new()
            {
                Latitude = 20,
                Longitude = 20,
                Timestamp = date.AddHours(1),
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            var location = await service.GetLastLocation("user1");

            // Assert
            Assert.That(location, Is.Not.Null);
            Assert.That(location.Latitude, Is.EqualTo(20));
            Assert.That(location.Longitude, Is.EqualTo(20));
        }

        [Test]
        public async Task GetLastLocations_ReturnsEmptyListIfNoUsersExist()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            userService.Setup(m => m.ListUsers()).ReturnsAsync([]);

            var service = new DefaultUserLocationService(userService.Object);

            // Act
            var locations = (await service.GetLastLocations()).ToList();

            // Assert
            Assert.That(locations, Is.Not.Null);
            Assert.That(locations, Is.Empty);
        }

        [Test]
        public async Task GetLastLocations_ReturnsMostRecentLocationsForAllUsers()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            userService.Setup(m => m.GetUser(It.IsAny<string>())).ReturnsAsync(new User());

            userService.Setup(m => m.ListUsers()).ReturnsAsync([
                new()
                {
                    Id = "user1",
                    Name = "user1",
                },
                new()
                {
                    Id = "user2",
                    Name = "user2",
                },
            ]);

            var service = new DefaultUserLocationService(userService.Object);

            var date = new DateTime(2024, 2, 16, 10, 58, 0);

            // Act
            await service.AddLocation(new()
            {
                Latitude = 10,
                Longitude = 10,
                Timestamp = date,
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            await service.AddLocation(new()
            {
                Latitude = 20,
                Longitude = 20,
                Timestamp = date.AddHours(1),
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            await service.AddLocation(new()
            {
                Latitude = 30,
                Longitude = 30,
                Timestamp = date,
                User = new()
                {
                    Id = "user2",
                    Name = "user2",
                },
            });

            var locations = (await service.GetLastLocations()).ToList();

            // Assert
            Assert.That(locations, Is.Not.Null);
            Assert.That(locations, Has.Count.EqualTo(2));

            Assert.That(locations[0].User.Id, Is.EqualTo("user1"));
            Assert.That(locations[0].Latitude, Is.EqualTo(20));
            Assert.That(locations[0].Longitude, Is.EqualTo(20));

            Assert.That(locations[1].User.Id, Is.EqualTo("user2"));
            Assert.That(locations[1].Latitude, Is.EqualTo(30));
            Assert.That(locations[1].Longitude, Is.EqualTo(30));
        }

        [Test]
        public async Task GetLocations_ReturnsEmptyListIfNoneExist()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            var service = new DefaultUserLocationService(userService.Object);

            // Act
            var locations = (await service.GetLocations("user1")).ToList();

            // Assert
            Assert.That(locations, Is.Not.Null);
            Assert.That(locations, Is.Empty);
        }

        [Test]
        public async Task GetLocations_ReturnsAllLocationsForUser()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            userService.Setup(m => m.GetUser(It.IsAny<string>())).ReturnsAsync(new User());

            var service = new DefaultUserLocationService(userService.Object);

            var date = new DateTime(2024, 2, 16, 10, 58, 0);

            // Act
            await service.AddLocation(new()
            {
                Latitude = 10,
                Longitude = 10,
                Timestamp = date,
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            await service.AddLocation(new()
            {
                Latitude = 20,
                Longitude = 20,
                Timestamp = date.AddHours(1),
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            var locations = (await service.GetLocations("user1")).ToList();

            // Assert
            Assert.That(locations, Is.Not.Null);
            Assert.That(locations, Has.Count.EqualTo(2));

            Assert.That(locations[0].Latitude, Is.EqualTo(10));
            Assert.That(locations[0].Longitude, Is.EqualTo(10));

            Assert.That(locations[1].Latitude, Is.EqualTo(20));
            Assert.That(locations[1].Longitude, Is.EqualTo(20));
        }
    }
}

using AirboxTechnical.Services;

namespace AirboxTechnical.Tests
{
    public class DefaultUserServiceTests
    {
        [Test]
        public async Task AddUser_SuccessfullyStoresUser()
        {
            // Arrange
            var service = new DefaultUserService();

            // Act
            await service.AddUser(new()
            {
                Id = "user1",
                Name = "user1",
            });

            // Assert
            var location = await service.GetUser("user1");

            Assert.That(location, Is.Not.Null);
        }

        [Test]
        public async Task GetUser_NoSuchUser_ReturnsNull()
        {
            // Arrange
            var service = new DefaultUserService();

            // Act
            var location = await service.GetUser("user1");

            // Assert
            Assert.That(location, Is.Null);
        }
    }
}

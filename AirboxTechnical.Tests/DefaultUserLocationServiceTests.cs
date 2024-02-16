using AirboxTechnical.Services;

namespace AirboxTechnical.Tests
{
    public class DefaultUserLocationServiceTests
    {
        [Test]
        public async Task GetLastLocation_ReturnsLastLocationIfItExists()
        {
            var service = new DefaultUserLocationService();

            await service.AddLocation(new()
            {
                Latitude = 10,
                Longitude = 10,
                Timestamp = DateTime.Now,
                User = new()
                {
                    Id = "user1",
                    Name = "user1",
                },
            });

            var location = await service.GetLastLocation("user1");

            Assert.That(location, Is.Not.Null);
            Assert.That(location.Latitude, Is.EqualTo(10));
            Assert.That(location.Longitude, Is.EqualTo(10));
        }
    }
}

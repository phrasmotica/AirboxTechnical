using AirboxTechnical.Validation;

namespace AirboxTechnical.Tests
{
    public class CreateUserLocationRequestValidatorTests
    {
        [Test]
        public void Validate_MissingUser_Fails()
        {
            var validator = new CreateUserLocationRequestValidator();

            var validation = validator.Validate(new Models.CreateUserLocationRequest()
            {
                Latitude = 5,
                Longitude = -5,
            });

            Assert.That(validation.IsValid, Is.False);
        }

        [Test]
        public void Validate_InvalidLatitude_Fails()
        {
            var validator = new CreateUserLocationRequestValidator();

            var validation = validator.Validate(new Models.CreateUserLocationRequest()
            {
                User = new(),
                Latitude = 200,
                Longitude = -5,
            });

            Assert.That(validation.IsValid, Is.False);
        }

        [Test]
        public void Validate_InvalidLongitude_Fails()
        {
            var validator = new CreateUserLocationRequestValidator();

            var validation = validator.Validate(new Models.CreateUserLocationRequest()
            {
                User = new(),
                Latitude = 5,
                Longitude = -200,
            });

            Assert.That(validation.IsValid, Is.False);
        }
    }
}

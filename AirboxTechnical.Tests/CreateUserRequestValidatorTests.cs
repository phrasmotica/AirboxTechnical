using AirboxTechnical.Validation;

namespace AirboxTechnical.Tests
{
    public class CreateUserRequestValidatorTests
    {
        [Test]
        public void Validate_NamePresent_Succeeds()
        {
            var validator = new CreateUserRequestValidator();

            var validation = validator.Validate(new Models.CreateUserRequest()
            {
                Name = "rolling",
            });

            Assert.That(validation.IsValid, Is.True);
        }

        [Test]
        public void Validate_NameMissing_Fails()
        {
            var validator = new CreateUserRequestValidator();

            var validation = validator.Validate(new Models.CreateUserRequest()
            {
                Name = string.Empty,
            });

            Assert.That(validation.IsValid, Is.False);
        }
    }
}

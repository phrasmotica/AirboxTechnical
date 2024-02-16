using AirboxTechnical.Models;
using FluentValidation;

namespace AirboxTechnical.Validation
{
    public class CreateUserLocationRequestValidator : AbstractValidator<CreateUserLocationRequest>
    {
        public CreateUserLocationRequestValidator()
        {
            RuleFor(r => r.User).NotNull();

            RuleFor(r => r.Latitude).GreaterThan(-90).WithMessage("Latitude value must be greater than -90");
            RuleFor(r => r.Latitude).LessThan(90).WithMessage("Latitude value must be less than 90");

            RuleFor(r => r.Longitude).GreaterThan(-180).WithMessage("Longitude value must be greater than -180");
            RuleFor(r => r.Longitude).LessThanOrEqualTo(180).WithMessage("Longitude value must be at most 180");
        }
    }
}

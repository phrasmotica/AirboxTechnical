using AirboxTechnical.Models;
using FluentValidation;

namespace AirboxTechnical.Validation
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("User's name must not be empty");
        }
    }
}

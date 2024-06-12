using FluentValidation;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Application.Authentication.Command.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginRequest>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}

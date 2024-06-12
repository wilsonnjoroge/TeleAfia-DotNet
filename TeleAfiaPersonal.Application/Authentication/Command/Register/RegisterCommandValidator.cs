using FluentValidation;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Application.Authentication.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(15);
            RuleFor(x => x.IdNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Location).NotEmpty().MinimumLength(6);
        }
    }
}

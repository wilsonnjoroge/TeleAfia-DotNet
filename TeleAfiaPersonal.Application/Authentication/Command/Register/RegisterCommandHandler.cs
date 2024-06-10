

using MediatR;
using TeleAfiaPersonal.Application.Common.interfaces;
using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Application.Authentication.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.IdNumber, request.Password);
            await _userRepository.AddAsync(user);
            // You may need additional logic here, such as sending confirmation emails
            return Unit.Value;
        }
    }
}

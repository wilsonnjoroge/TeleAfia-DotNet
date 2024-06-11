using MediatR;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;
using TeleAfiaPersonal.Application.Common.Interfaces;
using TeleAfiaPersonal.Application.Common.interfaces.Authentication;
using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Application.Authentication.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenService;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if the email already exists in the database
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("Email address already exists.");
            }

            // Hash the password before creating the user entity
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create a new user entity
            var user = new ApplicationUser(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.PhoneNumber,
                    request.Location,
                    request.IdNumber,
                    hashedPassword);

            // Add the user to the repository
            await _userRepository.AddAsync(user);

            // Generate the token
            var token = _tokenService.GenerateToken(user);

            // Create the response DTO
            var response = new AuthenticationResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IdNumber = user.IdNumber,
                Location = user.Location,
                Token = token
            };

            return response;
        }

    }
}

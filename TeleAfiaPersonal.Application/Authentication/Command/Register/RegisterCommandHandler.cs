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
            var Request = request.RegisterRequest;

            // Check if the email already exists in the database
            var existingUser = await _userRepository.GetByEmailAsync(Request.Email);
            if (existingUser != null)
            {
                throw new Exception("Email address already exists.");
            }

            // Hash the password before creating the user entity
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Request.Password);

            // Create a new user entity
            var user = new ApplicationUser(
                    Request.FirstName,
                    Request.LastName,
                    Request.Email,
                    Request.PhoneNumber,
                    Request.Location,
                    Request.IdNumber,
                    hashedPassword,
                    resetToken: null,
                    isDeleted: false,
                    isEmailConfirmed: false,
                    is2FAEnabled: false
                    );

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

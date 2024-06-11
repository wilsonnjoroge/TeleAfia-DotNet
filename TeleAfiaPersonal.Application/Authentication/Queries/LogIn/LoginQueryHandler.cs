
using MediatR;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;
using TeleAfiaPersonal.Application.Common.Interfaces;
using TeleAfiaPersonal.Application.Common.interfaces.Authentication;

namespace TeleAfiaPersonal.Application.Authentication.Command.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenService;

        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {

            
            // Check if the user exists with the given email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Verify the password using BCrypt
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Wrong Password");
            }
            

            // Generate JWT token
            var token = _tokenService.GenerateToken(user);

            // Return authentication response
            return new AuthenticationResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IdNumber = user.IdNumber,
                Location = user.Location,
                Token = token
            };
        }
    }
}

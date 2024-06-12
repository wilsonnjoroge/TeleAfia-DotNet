using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TeleAfiaPersonal.Application.Common.interfaces.Authentication;
using TeleAfiaPersonal.Application.Common.Interfaces;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;
using TeleAfiaPersonal.Contracts.MessageDTO;

namespace TeleAfiaPersonal.Application.Authentication.Command.ForgotPassword
{
    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenService;
        private readonly IEmailService _emailService;

        public ForgotPasswordQueryHandler(IUserRepository userRepository, IJwtTokenGenerator tokenService, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<ForgotPasswordResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.ForgotPasswordRequest == null)
            {
                throw new ArgumentException("Request cannot be null.");
            }

            var forgotPasswordRequest = request.ForgotPasswordRequest;

            // Fetch user by email
            var user = await _userRepository.GetByEmailAsync(forgotPasswordRequest.Email);
            if (user == null)
            {
                // It is more secure not to reveal if the user exists or not
                return new ForgotPasswordResponse
                {
                    Message = "If the email exists in our system, a password reset link has been sent."
                };
            }

            // Generate a password reset token
            var token = _tokenService.GenerateRandomToken(user);

            // Update the user's reset token
            user.SetResetToken(token);

            // Save the updated user entity to the repository
            await _userRepository.UpdateAsync(user);

            // Create the email message content
            var message = new Message(
                new[] { forgotPasswordRequest.Email },
                "Password Reset Token",
                $"Your password reset token is {token}");

            // Send the reset token email
            _emailService.SendEmail(message);

            // Prepare and return the response
            var response = new ForgotPasswordResponse
            {
                Token = token,
                Message = "A password reset token has been sent to your email."
            };

            return response;
        }
    }
}

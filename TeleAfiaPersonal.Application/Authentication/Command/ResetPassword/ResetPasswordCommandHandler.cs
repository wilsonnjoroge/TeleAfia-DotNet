
using MediatR;
using TeleAfiaPersonal.Application.Common.Interfaces;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Application.Authentication.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            var Request = request.ResetPasswordRequest;
            if (request == null)
            {
                throw new ArgumentException("Request cannot be null.");
            }

            // Fetch user by email
            var user = await _userRepository.GetByEmailAsync(Request.Email);
            if (user == null)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "Invalid email or token."
                };
            }

            // Verify the token
            if (user.ResetToken == null || !BCrypt.Net.BCrypt.Verify(user.ResetToken, Request.Token))
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "Invalid token."
                };
            }

            // Update the password and clear the reset token
            user.ChangePassword(Request.NewPassword);

            user.UpdateUserDetails(
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber,
                user.Location,
                user.IdNumber,
                user.IsEmailConfirmed,
                user.IsDeleted,
                null // Clear the reset token
            );

            // Persist the changes to the user
            await _userRepository.UpdateAsync(user);

            return new ResetPasswordResponse
            {
                Success = true,
                Message = "Password has been reset successfully."
            };
        }
    }
}

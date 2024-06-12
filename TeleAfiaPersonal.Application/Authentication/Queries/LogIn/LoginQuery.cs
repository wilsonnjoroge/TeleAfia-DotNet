using MediatR;
using System.ComponentModel.DataAnnotations;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Application.Authentication.Command.Login
{
    public class LoginQuery : IRequest<AuthenticationResponse>
    {
        public LoginRequest LoginRequest { get; set; }
    }
}

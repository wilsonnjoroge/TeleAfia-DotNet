using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Application.Authentication.Command.Register
{
    public class RegisterCommand : IRequest<AuthenticationResponse>
    {
       public RegisterRequest RegisterRequest { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Application.Authentication.Queries.ForgotPassword
{
    public class ForgotPasswordQuery : IRequest<ForgotPasswordResponse>
    {
        public ForgotPasswordRequest ForgotPasswordRequest { get; set; }
    }
}

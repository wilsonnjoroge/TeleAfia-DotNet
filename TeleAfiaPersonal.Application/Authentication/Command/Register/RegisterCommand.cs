using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleAfiaPersonal.Application.Authentication.Command.Register
{
    public class RegisterCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string Password { get; set; }
    }
}

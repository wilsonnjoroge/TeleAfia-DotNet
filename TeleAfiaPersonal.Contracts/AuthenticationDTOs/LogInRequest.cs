using System;

namespace TeleAfiaPersonal.Contracts.AuthenticationDTOs
{
    public class LogInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

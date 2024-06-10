using System;

namespace TeleAfiaPersonal.Contracts.AuthenticationDTOs
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string PasswordHash { get; set; }
    }
}

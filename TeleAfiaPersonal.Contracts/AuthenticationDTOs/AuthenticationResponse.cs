using System;

namespace TeleAfiaPersonal.Contracts.AuthenticationDTOs
{
    public class AuthenticationResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string Location { get; set; }
        public string Token { get; set; }
    }
}

﻿using System;

namespace TeleAfiaPersonal.Contracts.AuthenticationDTOs
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
    }
}

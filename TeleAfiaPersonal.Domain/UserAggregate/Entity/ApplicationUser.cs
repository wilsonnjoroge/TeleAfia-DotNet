


    namespace TeleAfiaPersonal.Domain.UserAggregate.Entity
    {
        public class ApplicationUser
        {
            private Guid _id;
            private DateTime _createdDate;
            private DateTime _updatedDate;

            // Properties
            public Guid Id => _id;
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string Email { get; private set; }
            public string PhoneNumber { get; private set; }
            public string IdNumber { get; private set; }
            public string Location { get; private set; }
            public string Password { get; private set; }
            public DateTime CreatedDate => _createdDate;
            public DateTime UpdatedDate => _updatedDate;

            // Constructor
            public ApplicationUser(string firstName, string lastName, string email, string phoneNumber, string idNumber, string location, string password)
            {
                _id = Guid.NewGuid();
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                PhoneNumber = phoneNumber;
                IdNumber = idNumber;
                Location = location;
                Password = password; // No hashing here
                _createdDate = DateTime.UtcNow;
                _updatedDate = DateTime.UtcNow;
            }

            // Methods for updating properties
            public void UpdateUserDetails(string firstName, string lastName, string email, string phoneNumber, string location, string idNumber)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                PhoneNumber = phoneNumber;
                IdNumber = idNumber;
                Location = location;
                _updatedDate = DateTime.UtcNow;
            }

            public void ChangePassword(string newPassword)
            {
                Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _updatedDate = DateTime.UtcNow;
            }

            public void UpdateEmail(string email)
            {
                Email = email;
                _updatedDate = DateTime.UtcNow;
            }
        }
    }



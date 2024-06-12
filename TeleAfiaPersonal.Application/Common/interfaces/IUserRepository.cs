using System.Threading.Tasks;
using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(ApplicationUser user);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task UpdateAsync(ApplicationUser user); // Add this method for updating the user
        // Method to verify 2FA code

    }
}

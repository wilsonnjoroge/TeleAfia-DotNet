using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(ApplicationUser user);
        Task<ApplicationUser> GetByEmailAsync(string email);
        
    }
}

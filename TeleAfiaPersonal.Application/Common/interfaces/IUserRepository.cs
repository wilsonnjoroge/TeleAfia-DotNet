
using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Application.Common.interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(ApplicationUser user);

    }
}

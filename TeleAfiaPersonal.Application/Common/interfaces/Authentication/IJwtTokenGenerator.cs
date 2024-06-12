


using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Application.Common.interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);

        string GenerateRandomToken(ApplicationUser user);
    }
}


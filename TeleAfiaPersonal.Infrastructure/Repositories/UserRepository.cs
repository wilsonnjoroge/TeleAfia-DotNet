using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeleAfiaPersonal.Application.Common.Interfaces;
using TeleAfiaPersonal.Domain.UserAggregate.Entity;

namespace TeleAfiaPersonal.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Implement other methods for user management
    }
}

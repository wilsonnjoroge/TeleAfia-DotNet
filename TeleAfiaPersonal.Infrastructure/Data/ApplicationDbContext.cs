
using Microsoft.EntityFrameworkCore;
using TeleAfiaPersonal.Domain.UserAggregate.Entity;
using TeleAfiaPersonal.Infrastructure.EntityConfigurations;


namespace TeleAfiaPersonal.Infrastructure
{
    

    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

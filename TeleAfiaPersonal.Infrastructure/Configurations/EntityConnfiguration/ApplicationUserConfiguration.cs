// File path: TeleAfiaPersonal.Infrastructure/EntityConfigurations/ApplicationUserConfiguration.cs
namespace TeleAfiaPersonal.Infrastructure.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeleAfiaPersonal.Domain.UserAggregate.Entity; // Adjust to the namespace of your domain entity

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);

            // Map the private fields for CreatedDate and UpdatedDate
            builder.Property(u => u.CreatedDate)
                .HasField("_createdDate")
                .IsRequired();

            builder.Property(u => u.UpdatedDate)
                .HasField("_updatedDate")
                .IsRequired();

            // Other property configurations as needed
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(u => u.IdNumber)
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}

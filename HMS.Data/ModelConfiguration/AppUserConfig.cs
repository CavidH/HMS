using HMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Data.ModelConfiguration
{
     class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}

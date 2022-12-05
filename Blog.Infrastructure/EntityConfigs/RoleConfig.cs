using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityConfigs
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Role)
                .HasPrincipalKey(x => x.ID)
                .HasForeignKey(x => x.RoleID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Role_User");
        }
    }
}

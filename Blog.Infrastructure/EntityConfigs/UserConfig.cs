using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EntityConfigs
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Articles)
                .WithOne(x => x.User)
                .HasPrincipalKey(x => x.ID)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Articles");

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasPrincipalKey(x => x.ID)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_User_Comment");
        }
    }
}

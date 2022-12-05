using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityConfigs
{
    internal class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Article)
                .HasPrincipalKey(x => x.ID)
                .HasForeignKey(x => x.ArticleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Article_Comment");

            builder.HasMany(x => x.Tags)
                .WithMany(x => x.Articles);
        }
    }
}

using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Contexts
{
    public class BlogContext : DbContext
    {
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .HasOne(x => x.User)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Article_User");

            modelBuilder.Entity<Comment>()
                .HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Comment_User");

            modelBuilder.Entity<Article>()
                .HasMany(x => x.Tags)
                .WithMany(x => x.Articles);
        }
    }
}

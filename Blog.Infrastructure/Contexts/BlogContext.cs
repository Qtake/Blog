using Blog.Domain.Entities;
using Blog.Domain.EntityConfigs;
using Blog.Infrastructure.EntityConfigs;
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

            modelBuilder.ApplyConfiguration(new ArticleConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}

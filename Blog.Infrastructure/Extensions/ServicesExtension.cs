using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Infrastructure.Contexts;
using Blog.Infrastructure.Repositories;
using Blog.Service.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("Default") ?? "Default";

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connection));
            services.AddRepositoryDependensies();

            return services;
        }

        private static IServiceCollection AddRepositoryDependensies(this IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<BlogContext>();
            string[] roles = Enum.GetNames(typeof(RoleType));

            foreach (var role in roles)
            {
                if (!context.Roles.Any(x => x.Name == role))
                {
                    context.Roles.Add(new Role(role));
                }
            }

            context.SaveChanges();

            //foreach (var user in context.Users)
            //{
            //    if (!context.Users.Any(x => x.Name == "Admin1"))
            //    {
            //        var admin = new User
            //        {
            //            Name = "Admin1",
            //            Email = "Debik228@gmail.com",
            //            Password = "123456",
            //            Role = new Role { Name = RoleType.Admin.ToString() }
            //        };
            //    }
            //}

            //context.SaveChanges();

            return app;
        }

    }
}

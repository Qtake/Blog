using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
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

            var admin = new User
            {
                Name = "Admin1",
                Email = "Admin@gmail.com",
                Password = "123456",
                Role = context.Roles.First(x => x.Name == RoleType.Admin.ToString())
            };

            if (!context.Users.Any())
            {
                context.Users.Add(admin);
            }

            context.SaveChanges();

            return app;
        }
    }
}

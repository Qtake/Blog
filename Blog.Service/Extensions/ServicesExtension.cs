using Blog.Service.Services;
using Blog.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Service.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddServiceDependensies();
            services.AddMappers();

            return services;
        }

        private static IServiceCollection AddServiceDependensies(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            

            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

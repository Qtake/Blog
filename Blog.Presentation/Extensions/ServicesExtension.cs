using Blog.Infrastructure.Extensions;
using Blog.Service.Extensions;

namespace Blog.Presentation.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddService();

            return services;
        }
    }
}

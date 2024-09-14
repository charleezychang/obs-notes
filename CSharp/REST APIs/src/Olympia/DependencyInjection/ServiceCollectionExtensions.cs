using Microsoft.EntityFrameworkCore;
using Olympia.Persistence;

namespace Olympia.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        // 'this' keyword is an extension method, meaning it is a static method that is called using the class name
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ProductsService>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OlympiaContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}

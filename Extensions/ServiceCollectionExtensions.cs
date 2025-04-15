using ProductCatalog.Interfaces.Repositories;
using ProductCatalog.Repositories;

namespace ProductCatalog.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}

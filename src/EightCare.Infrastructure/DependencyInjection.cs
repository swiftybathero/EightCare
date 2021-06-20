using EightCare.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace EightCare.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddDbContext<KeeperContext>();
        }
    }
}

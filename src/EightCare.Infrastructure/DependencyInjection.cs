using EightCare.Application.Common.Interfaces;
using EightCare.Infrastructure.Persistence;
using EightCare.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EightCare.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddDbContext<KeeperContext>()
                           .AddScoped<IKeeperRepository, KeeperRepository>();
        }
    }
}

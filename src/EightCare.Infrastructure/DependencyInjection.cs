using EightCare.Application.Common.Interfaces;
using EightCare.Infrastructure.Common.Configuration;
using EightCare.Infrastructure.Persistence;
using EightCare.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EightCare.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<CollectionContext>(builder =>
                           {
                               builder.UseSqlServer(configuration.GetSection(DatabaseConfiguration.Key)
                                                                 .Get<DatabaseConfiguration>().ConnectionString);
                           })
                           .AddScoped<ICollectionRepository, CollectionRepository>();
        }
    }
}

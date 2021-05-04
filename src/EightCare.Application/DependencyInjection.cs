using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EightCare.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}

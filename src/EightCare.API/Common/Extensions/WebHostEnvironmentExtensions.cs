using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EightCare.API.Common.Extensions
{
    public static class Environments
    {
        public const string FunctionalTest = "FunctionalTest";
    }

    public static class WebHostEnvironmentExtensions
    {
        public static bool IsFunctionalTest(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment(Environments.FunctionalTest);
        }
    }
}

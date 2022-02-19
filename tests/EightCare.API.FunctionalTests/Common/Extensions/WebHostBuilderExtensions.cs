using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EightCare.API.FunctionalTests.Common.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureTestDatabase(this IWebHostBuilder builder)
        {
            return ShouldRunAgainstProductionDatabase()
                ? builder
                : builder.AppendConfigFromFile("appsettings.Development.json");
        }

        private static IWebHostBuilder AppendConfigFromFile(this IWebHostBuilder builder, string jsonFileName)
        {
            return builder.ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile(jsonFileName);
            });
        }

        private static bool ShouldRunAgainstProductionDatabase()
        {
            return bool.TryParse(Environment.GetEnvironmentVariable("RunAgainstProductionDatabase"),
                out var testProductionDatabase) && testProductionDatabase;
        }
    }
}

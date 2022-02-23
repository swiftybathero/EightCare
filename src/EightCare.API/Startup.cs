using EightCare.Application;
using EightCare.Application.Common.Exceptions;
using EightCare.Domain.Exceptions;
using EightCare.Infrastructure;
using EightCare.Infrastructure.Persistence;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace EightCare.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails(ConfigureProblemDetails);
            services.AddControllers()
                    .AddProblemDetailsConventions()
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EightCare API",
                    Version = "v1"
                });
            });

            services.AddApplication();
            services.AddInfrastructure(Configuration);
        }

        private void ConfigureProblemDetails(ProblemDetailsOptions options)
        {
            options.IncludeExceptionDetails = (_, _) => Environment.IsDevelopment();

            options.MapToStatusCode<CollectionDomainException>(StatusCodes.Status400BadRequest);
            options.MapToStatusCode<EntityNotFoundException>(StatusCodes.Status404NotFound);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CollectionContext context)
        {
            app.UseProblemDetails();

            if (env.IsDevelopment())
            {
                app.UseSwagger()
                   .UseSwaggerUI(setup =>
                   {
                       setup.SwaggerEndpoint("/swagger/v1/swagger.json", "EightCare API V1");
                       setup.RoutePrefix = string.Empty;
                   });

                // Recreating database for development purposes
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
        }
    }
}

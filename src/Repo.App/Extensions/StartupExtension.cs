using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repo.App.Contracts;
using Repo.App.Repositories;

namespace Repo.App.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SampleContext>(options =>
                options.UseNpgsql(config.GetConnectionString("SampleContext")));

            services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
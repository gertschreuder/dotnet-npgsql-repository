using System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repo.App.Extensions;
using Serilog;

namespace Repo.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRepositories(_configuration);

            services.AddControllers();

            services.AddHealthChecks()
                .AddNpgSql(_configuration.GetConnectionString("SampleContext"), name: "Postgres DB", tags: new[] { "db" })
                .AddProcessAllocatedMemoryHealthCheck(maximumMegabytesAllocated: 16384, name: "Process Allocated Memory", tags: new[] { "memory" })
                .AddPrivateMemoryHealthCheck(maximumMemoryBytes: 16384000000, name: "Private Memory", tags: new[] { "memory" })
                .AddVirtualMemorySizeHealthCheck(maximumMemoryBytes: 16384000000000, name: "Virtual Memory", tags: new[] { "memory" })
               // .AddUrlGroup(new Uri("https://localhost:5001/health"), "Example endpoint", tags: new[] { "ping" })
                ;
            services.AddHealthChecksUI(s =>
                    {
                       // s.AddHealthCheckEndpoint("Sample", "https://localhost:5001/health");
                    }
                    )
                .AddPostgreSqlStorage(_configuration.GetConnectionString("SampleContext"));

            services.AddCustomSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Api V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(setup =>
                {
                    // https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/blob/master/doc/styles-branding.md
                    setup.AddCustomStylesheet("health-ui.css");

                });
            });
        }
    }
}
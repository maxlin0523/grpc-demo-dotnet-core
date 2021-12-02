using GrpcDemo.DomainService.Core.Interfaces.Repositories;
using GrpcDemo.DomainService.Core.Interfaces.Services;
using GrpcDemo.DomainService.Core.Repositories;
using GrpcDemo.DomainService.Core.Services;
using GrpcDemo.DomainService.Core.Utilities.DatabaseHelpers;
using GrpcDemo.DomainService.Implements;
using GrpcDemo.DomainService.Utilities.DependencyInjectionExtensions;
using GrpcDemo.DomainService.Utilities.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GrpcDemo.DomainService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                // Exception handler
                options.Interceptors.Add<ExceptionInterceptor>();
            });

            // Services
            services.AddSingleton<ICompanyService, CompanyService>();

            // Repositories
            services.AddSingleton<ICompanyRepository, CompanyRepository>();

            // Database
            services.AddDatabaseHelperExtension(Configuration);

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CompanyServiceImp>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}

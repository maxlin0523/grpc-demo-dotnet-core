using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcDemo.Message;
using Grpc.Core;
using Microsoft.OpenApi.Models;
using GrpcDemo.WebApplication.Utilities.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using GrpcDemo.Common.Models;
using ResponseCode = GrpcDemo.Common.Enums.ResponseCode;
using Microsoft.AspNetCore.Http;

namespace GrpcDemo.WebApplication
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "GrpcDemo.WebApplication",
                    Version = "v1" 
                });
            });

            services.AddControllers();

            // gRPC Service
            var host = Configuration.GetSection("DomainService:Host").Value;
            var port = Configuration.GetSection("DomainService:Port").Value;
            services.AddSingleton(new CompanyService.CompanyServiceClient(new Channel($"{host}:{port}", ChannelCredentials.Insecure)));

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrpcDemo.WebApplication v1"); 
                });
            }

            // Exception handler
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

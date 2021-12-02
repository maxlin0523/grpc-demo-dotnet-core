using GrpcDemo.DomainService.Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Utilities.DependencyInjectionExtensions
{
    public static class DatabaseHelperExtension
    {

        public static IServiceCollection AddDatabaseHelperExtension(this IServiceCollection services, IConfiguration config)
        {
            // SQL Server
            var connectionString = config.GetSection("Sql:ConnectionString").Value;
            var host = config.GetSection("Sql:Host").Value;
            var port = config.GetSection("Sql:Port").Value;
            var database = config.GetSection("Sql:Database").Value;
            services.AddSingleton(
                new SqlConnectionHelper(string.Format(connectionString, host, port, database)));

            // Redis
            services.AddSingleton(
                new RedisConnectionHelper(config.GetSection("Redis:ConnectionString").Value));

            return services;
        }
    }
}

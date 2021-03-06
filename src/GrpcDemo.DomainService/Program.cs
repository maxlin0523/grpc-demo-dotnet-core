using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                     .ConfigureKestrel((context, kestrelOptions) =>
                     {
                         var port = context.Configuration.GetValue<int>("DomainService:Port");
                         kestrelOptions.ListenAnyIP(port, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
                     });
                });
    }
}

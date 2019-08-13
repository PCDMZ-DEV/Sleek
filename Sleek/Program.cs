using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Sleek {

    public class Program {
        
        public static void Main(string[] args) {

            // Access appsettings.json from Program.cs
            var Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            // Create Logger
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.MSSqlServer(Configuration["Serilog:ConnectionString"], Configuration["Serilog:TableName"], autoCreateSqlTable: true)
                .WriteTo.Console()
                .CreateLogger();
            
            try {
                CreateWebHostBuilder(args).Build().Run();
            } finally {
                Log.CloseAndFlush();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();

    }

}

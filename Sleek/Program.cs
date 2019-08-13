using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

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
                .MinimumLevel.Warning()
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

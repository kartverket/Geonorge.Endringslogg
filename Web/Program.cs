using System;
using System.IO;
using Geonorge.Endringslogg.Web.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Geonorge.Endringslogg.Web
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static int Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RollingFile(Configuration["AppSettings:LogFile"])
                .CreateLogger();

            try
            {
                var host = BuildWebHost(args);

                MigrateAndSeedDatabase(host);

                Log.Information("Starting web host");
                host.Run();

                return 0;

            }

            catch (Exception ex)

            {
                Log.Fatal(ex, "Host terminated unexpectedly");

                return 1;
            }

            finally

            {
                Log.CloseAndFlush();
            }
        }

        private static void MigrateAndSeedDatabase(IWebHost host)
        {
            Log.Information("Running migrations and seeding data");

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                    DbInitializer.Initialize(context);
                }

                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while seeding the database.");
                }

            }

        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();

        }
    }
}
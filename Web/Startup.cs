using Geonorge.Endringslogg.Web.ActionFilters;
using Geonorge.Endringslogg.Web.Data;
using Geonorge.Endringslogg.Web.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Geonorge.Endringslogg.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddApplicationInsightsTelemetry();

            if (_environment.IsDevelopment())
                services.Configure<TelemetryConfiguration>(options => options.DisableTelemetry = true);

            services.AddTransient<LogEntryService>();
            services.AddTransient<IApplicationService, ApplicationService>();

            AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddSingleton(appSettings);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddScoped<LogHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
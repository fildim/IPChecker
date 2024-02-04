using FluentValidation;
using Hangfire;
using Serilog;
using IPChecker.DTOS.SearchIPAddressDTOS;
using IPChecker.Models;
using IPChecker.Services;
using IPChecker.Validators;
using IPChecker.Middleware;
using Microsoft.EntityFrameworkCore;

namespace IPChecker
{
    public class Program
    {

        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<IpcheckerDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("IPCHECKERDB")));

            

            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Database=Hangfire.IPChecker; Integrated Security=True");

            var server = new BackgroundJobServer();

            //RecurringJob.AddOrUpdate(() => new UpdateBackgroundService().Update(), Cron.Hourly);

            builder.Services.AddTransient<LoggingMiddleware>();

            builder.Services.AddHttpClient();
            
            builder.Services.AddMemoryCache();

            builder.Services.AddControllers();

            builder.Services.AddScoped<IValidator<InputIPAddressDTO>, IPAddressInputValidator>();

            builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });





            var app = builder.Build();

            // Configure the HTTP request pipeline.


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
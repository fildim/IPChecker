using FluentValidation;
using Hangfire;
using Serilog;
using IPChecker.DTOS.SearchIPAddressDTOS;
using IPChecker.Models;
using IPChecker.Services;
using IPChecker.Validators;
using IPChecker.Middleware;
using Microsoft.EntityFrameworkCore;
using IPChecker.Repositories;
using Hangfire.MemoryStorage;

namespace IPChecker
{
    public class Program
    {


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<IpcheckerDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("IPCHECKERDB")));



            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());
                //.UseSqlServerStorage(builder.Configuration.GetConnectionString("IPCHECKERDB")));


            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<UpdateBackgroundService>();


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

            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IIpAddressRepository, IpAddressRepository>();

            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<IIpAddressService, IpAddressService>();
            builder.Services.AddScoped<IIP2CService, IP2CService>();
            builder.Services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            builder.Services.AddScoped<IMyService, MyService>();
            builder.Services.AddScoped<IReportService, ReportService>();





            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });


            app.UseMiddleware<LoggingMiddleware>();

            app.Lifetime.ApplicationStarted.Register(() =>
            {
                using var var1 = app.Services.CreateScope();

                var service = var1.ServiceProvider.GetRequiredService<UpdateBackgroundService>();
                RecurringJob.AddOrUpdate("update", () => service.Update(), Cron.Hourly, new RecurringJobOptions());

            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
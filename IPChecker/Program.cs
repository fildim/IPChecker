using FluentValidation;
using IPChecker.DTOS.SearchIPAddressDTOS;
using IPChecker.Validators;

namespace IPChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddHttpClient();
            
            builder.Services.AddMemoryCache();

            builder.Services.AddControllers();

            builder.Services.AddScoped<IValidator<InputIPAddressDTO>, IPAddressInputValidator>();





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
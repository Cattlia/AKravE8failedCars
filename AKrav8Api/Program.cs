using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AKrav8Api.Data;
using AKrav8Api.Controllers;
using DotNetEnv; // Make sure you have the DotNetEnv NuGet package installed
using Swashbuckle.AspNetCore.Annotations;

namespace AKrav8Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load environment variables from .env file
            Env.Load();

            // Get connection string from configuration
            string connectionString = Environment.GetEnvironmentVariable("DefaultConnection")     
                                            ?? 
            "Server=localhost;Port=3306;User=root;Password=User1p;Database=Vehicledatabase;";

            Console.WriteLine($"Using connection string: {connectionString}"); // Debugging

            builder.Services.AddDbContext<VehicleContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AKrav8 API", Version = "v1" });
                c.EnableAnnotations();
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("http://localhost:8080", "https://localhost:8081")  // Allow requests from the Swagger UI
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Debug);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AKrav8 API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}

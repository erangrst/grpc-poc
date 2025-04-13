using Grpc.Core;
using GrpcServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GreeterServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Server GRPC");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            builder.Services.AddGrpc();
            builder.Services.AddControllers();  // new
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors();

            app.UseGrpcWeb(new GrpcWebOptions {  DefaultEnabled = true });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapGrpcService<GreeterService>().EnableGrpcWeb();  // Register the GreeterService

            app.UseHttpsRedirection();

            app.MapControllers();

            //app.Urls.Add("https://localhost:7001");

            //var summaries = new[]
            //{
            //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            //};

            //app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            //{
            //    var forecast = Enumerable.Range(1, 5).Select(index =>
            //        new WeatherForecast
            //        {
            //            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //            TemperatureC = Random.Shared.Next(-20, 55),
            //            Summary = summaries[Random.Shared.Next(summaries.Length)]
            //        })
            //        .ToArray();
            //    return forecast;
            //})
            //.WithName("GetWeatherForecast")
            //.WithOpenApi();

            //app.MapGet("/", () => "Use a gRPC client to communicate with the server.");

            app.Run();
        }
    }
}

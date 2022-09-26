using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Reflection;
using VendingMachine.App.CommandHandlers;
using VendingMachine.App.Commands;
using VendingMachine.App.Contracts;
using VendingMachine.App.Dtos;
using VendingMachine.App.Models;
using VendingMachine.Data;

namespace VendingMachine.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
             .AddEnvironmentVariables()
             .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .ReadFrom.Configuration(configurationBuilder)
                .Enrich.FromLogContext()
                .WriteTo.File($"{AppContext.BaseDirectory}/Logs/logs.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}]|[{SourceContext}] {Message:lj}{NewLine}{Exception}",
                    fileSizeLimitBytes: 104857600,
                    rollingInterval: RollingInterval.Month,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            try
            {
                Log.ForContext<Program>().Information("Starting application up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.ForContext<Program>().Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostingContext, services) =>
            {
                services.AddSingleton<IRequestHandler<EnterCoinCommand, CommandResponse>, EnterCoinCommandHandler>();
                services.AddSingleton<IRequestHandler<CancelCommand, DevolutionResponse>, CancelCommandHandler>();
                services.AddSingleton<IRequestHandler<SelectProductCommand, DevolutionResponse>, SelectProductCommandHandler>();
                services.AddMediatR(Assembly.GetExecutingAssembly());
                services.AddSingleton<IMachine, Machine>();
                services.AddSingleton<ICoinsGetRepository, CoinsGetRepository>();
                services.AddSingleton<ICoinsAmountGetRepository, CoinsAmountGetRepository>();
                services.AddSingleton<IProductsGetRepository, ProductsGetRepository>();
                services.AddSingleton<IHostedService, Controller>();
            });
    }
}



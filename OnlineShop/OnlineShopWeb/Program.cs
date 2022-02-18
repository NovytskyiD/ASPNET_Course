using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using Serilog;
using Serilog.Events;
using System;

namespace OnlineShopWeb
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    IdentityInitializer.Initialize(userManager, roleManager);
                }
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingcontext, loggerconfiguration)=>
                {
                    loggerconfiguration
                    .ReadFrom.Configuration(hostingcontext.Configuration).Enrich.FromLogContext().Enrich.WithProperty("ApplicationName", "Online Shop");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

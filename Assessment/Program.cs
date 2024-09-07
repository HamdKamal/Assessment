using Databases.Data;
using Databases.Models.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Assessment
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();           

            using (var scope = host.Services.CreateScope())
            {

                var AccountContext = scope.ServiceProvider.GetRequiredService<DatabaseDbContext>();
                AccountContext.Database.Migrate();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<Users>>();
                var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ColumnRole>>();
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    await ContextSeed.SeedRolesAsync(userManager, _roleManager);
                    await ContextSeed.SeedAdminAsync(userManager, _roleManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               }).UseSerilog();
    }


}

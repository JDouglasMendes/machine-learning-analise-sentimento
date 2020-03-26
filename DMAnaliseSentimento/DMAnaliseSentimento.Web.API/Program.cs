using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMAnaliseSentimento.Web.API.EntityContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DMAnaliseSentimento.Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {                
                var context = services.GetRequiredService<DMAnaliseSentimentoContext>();
                var env = services.GetRequiredService<IConfiguration>();
                DbInitializer.SeedDataSet(context, env);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "ERRO AO INICIAR BASE DE DADOS.");
            }            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

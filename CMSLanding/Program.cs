using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if (UseNLog)
using OrchardCore.Logging;
#endif
#if (UseSerilog)
using Serilog;
#endif

namespace CMSLanding
{
    public class Program
    {
        public static Task Main(string[] args)
            => BuildHost(args).RunAsync();

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.ClearProviders())
#if (UseSerilog)
                .UseSerilog((hostingContext, configBuilder) =>
                    {
                        configBuilder.ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext();
                    })
#endif
                .ConfigureWebHostDefaults(webBuilder => webBuilder
#if (UseNLog)
                    .UseNLogWeb()
#endif
                    .UseStartup<Startup>()
                ).Build();
    }

    /*
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
*/
}

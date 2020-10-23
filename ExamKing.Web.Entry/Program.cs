using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ExamKing.Web.Entry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Inject()
                        .UseStartup<Startup>();
                });
        }
    }
}

using Furion.UnifyResult;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RESTfulResultProvider = ExamKing.Core.Providers.RESTfulResultProvider;

namespace ExamKing.WebApp.Admin
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsAccessor();
            services
                .AddControllersWithViews()
                .AddUnifyResult<RESTfulResultProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsAccessor();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
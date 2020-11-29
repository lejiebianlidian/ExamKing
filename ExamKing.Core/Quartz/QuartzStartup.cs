using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.Core
{
    /// <summary>
    /// QuartzNet Startup
    /// </summary>
    [AppStartup(700)]    
    public class QuartzStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
    }
}
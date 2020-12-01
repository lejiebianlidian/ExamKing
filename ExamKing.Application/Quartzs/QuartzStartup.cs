using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace ExamKing.Student.Quartz
{
    /// <summary>
    /// Quartz Startup
    /// </summary>
    [AppStartup(700)]
    public class QuartzStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //注册调度器工厂
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
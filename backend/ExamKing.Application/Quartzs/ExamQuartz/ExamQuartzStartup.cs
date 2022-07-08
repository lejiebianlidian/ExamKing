using ExamKing.Application.Quartz;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;

namespace ExamKing.Student.Quartz
{
    /// <summary>
    /// QuartzNet Startup
    /// </summary>
    [AppStartup(600)]
    public class ExamQuartzStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, ExamJobFactory>();
            //注册调度器工厂
            services.AddSingleton<JobRunner>();
            // 注册考试任务
            services.AddScoped<ExamJob>();
            services.AddHostedService<ExamQuartzHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

    }
}
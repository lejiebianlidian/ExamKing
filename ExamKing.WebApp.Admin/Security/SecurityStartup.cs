using System;
using Fur;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExamKing.WebApp.Admin
{
    [AppStartup(800)]
    public sealed class SecurityStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 管理员模块鉴权
            services.AddAppAuthorization<JWTAuthorizationHandler>(options =>
            {
                options.AddJWTAuthorization();
            });

        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
        
    }
}
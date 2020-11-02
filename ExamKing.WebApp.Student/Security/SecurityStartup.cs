using Fur;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.WebApp.Student
{
    [AppStartup(800)]
    public sealed class SecurityStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 学生模块鉴权
            services.AddJwt<JWTAuthorizationHandler>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
        
    }
}
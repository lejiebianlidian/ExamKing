using Fur;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.WebApp.Teacher
{
    [AppStartup(800)]
    public sealed class SecurityStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加JWT授权
            services.AddJwt<JwtAuthorizationHandler>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
        
    }
}
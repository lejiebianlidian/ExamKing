using Furion;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.Database.Core
{
    /// <summary>
    /// Database Startup
    /// </summary>
    [AppStartup(700)]
    public sealed class DatabaseCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<ExamDbContext>($"{DbProvider.MySql}@8.0.22");
            }, "ExamKing.Database.Migrations");
        }
    }
}

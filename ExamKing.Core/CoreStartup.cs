using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.Core
{
    /// <inheritdoc />
    [AppStartup(800)]
    public sealed class CoreStartup : AppStartup
    {
        /// <summary>
        /// ExamKingCore ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
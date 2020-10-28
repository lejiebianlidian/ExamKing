using Fur;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.Core
{
    /// <inheritdoc />
    [AppStartup(800)]
    public sealed class ExamKingCoreStartup : AppStartup
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
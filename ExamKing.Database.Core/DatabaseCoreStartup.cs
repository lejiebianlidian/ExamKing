﻿using Fur;
using Fur.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace ExamKing.Database.Core
{
    [AppStartup(600)]
    public sealed class DatabaseCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<ExamDbContext>(DbProvider.MySql);
            });
        }
    }
}
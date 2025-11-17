using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Infrastructure.Common;
using ChatAppSlnVersionII.Infrastructure.Common.ChatAppSlnVersionII.Infrastructure.ConfigService;
using ChatAppSlnVersionII.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Infrastructure.ConfigService
{
    public static class InfraRegisterService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettingsSection = configuration.GetSection("DatabaseSettings");
            services.Configure<DatabaseSettings>(dbSettingsSection);

            var dbSettings = dbSettingsSection.Get<DatabaseSettings>() ?? new DatabaseSettings();
            services.AddSingleton(dbSettings);

            var connectionString = dbSettings.BuildConnectionString();

            services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
            services.AddScoped<IDataAccess, DataAccess>();
            return services;
        }
    }
}

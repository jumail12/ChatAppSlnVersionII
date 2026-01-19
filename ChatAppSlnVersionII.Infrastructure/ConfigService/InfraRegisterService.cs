using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ChatAppSlnVersionII.Infrastructure.ConfigService
{
    public static class InfraRegisterService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataAccess, DataAccess>();

            services.AddSignalR();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}

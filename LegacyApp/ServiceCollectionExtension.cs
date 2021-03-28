using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //TODO : add a jsone config file to save the connection key
            services.AddSingleton<IDConnectionFactory>( new DbConnectionFactory("appConnectionKey"));
            services.AddSingleton<IClientRepository, ClientRepository>();

            ServiceLocator.ServiceProvider = service;
        }

        public static Configure()

    }
}
 
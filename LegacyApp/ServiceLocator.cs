using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public static class ServiceLocator
    {
        public static IServiceProvider ServiceProvider { get; set; }

        static ServiceProviderFactory(IServiceCollection services)
        {
            HostingEnvironment env = new HostingEnvironment();
            
            //TODO : Fix for your environnement
            Startup startup = new Startup(env);
            startup.ConfigureServices(services);
            ServiceProvider = sc.BuildServiceProvider();
        }
    }
}

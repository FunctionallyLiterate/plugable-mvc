using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlugableMvc.Events
{
    public static class Startup
    {
        public static IServiceCollection RegisterEventHandler<T>(this IServiceCollection services) where T : class, IEventHandler
        {
            services.AddTransient<IEventHandler, T>();
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using PlugableMvc.Plugins;
using PlugableMvc.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlugableMvc.Events
{
    public class EventsPlugin : PluginBase
    {
        public override bool UseStaticFiles => false;
        public override string Name => "Events";

        public override List<ConfigureServicesAction> ConfigureServiceActions => new List<ConfigureServicesAction>
        {
            new ConfigureServicesAction
            {
                Priority = 1,
                ConfigureServices = (services) => {
                    services.AddTransient<IEventBroadcaster, EventBroadcaster>();
                }
            }
        };
    }
}

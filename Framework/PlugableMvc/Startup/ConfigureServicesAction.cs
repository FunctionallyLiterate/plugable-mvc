using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlugableMvc.Startup
{
    public class ConfigureServicesAction
    {
        public int Priority { get; set; } = 1;
        public Action<IServiceCollection> ConfigureServices { get; set; }
    }
}

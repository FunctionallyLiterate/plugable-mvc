using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using PlugableMvc.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugableMvc.Plugins
{
    public abstract class PluginBase
    {
        public PluginBase()
        {

        }

        public abstract bool UseStaticFiles { get; }

        public virtual string StaticAssetFolderName
        {
            get
            {
                return "wwwroot";
            }
        }

        public virtual string StaticAssetRoutePrefix
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// Gets the name of the extension.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public virtual List<ConfigureServicesAction> ConfigureServiceActions { get; } = new List<ConfigureServicesAction>();
        public virtual List<ConfigureAction> ConfigureActions { get; } = new List<ConfigureAction>();
    }
}

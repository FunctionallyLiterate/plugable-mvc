using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using PlugableMvc.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugableMvc.Everything
{
    public class EverythingPlugin : PluginBase
    {
        public override string Name => "Everything";

        public override bool UseStaticFiles => true;
    }
}

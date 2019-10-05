using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PlugableMvc.Plugins
{
    public interface IPluginLocator
    {
        void Load();
        Dictionary<PluginBase, Assembly> Locate();
    }
}

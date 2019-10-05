using Microsoft.Extensions.DependencyModel;
using PlugableMvc.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PlugableMvc.Hosting.Plugins
{
    public class StaticPluginLocator : IPluginLocator
    {
        private static Dictionary<PluginBase, Assembly> _plugins = null;
        private static object _lockObj = new object();

        private List<PluginBase> _pluginList;
        public StaticPluginLocator(Action<List<PluginBase>> plugins)
        {
            _pluginList = new List<PluginBase>();
            plugins.Invoke(_pluginList);
        }

        public void Load()
        {
            lock (_lockObj)
            {
                if (_plugins == null)
                    loadPlugins();
            }

        }
        public Dictionary<PluginBase, Assembly> Locate()
        {
            if (_plugins == null)
                _plugins = new Dictionary<PluginBase, Assembly>();

            return _plugins;
        }

        private void loadPlugins()
        {
            _plugins = new Dictionary<PluginBase, Assembly>();
            foreach (var p in _pluginList)
                _plugins.Add(p, p.GetType().Assembly);
        }
    }
}

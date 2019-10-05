using Microsoft.Extensions.DependencyModel;
using PlugableMvc.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PlugableMvc.Hosting.Plugins
{
    public class ReferencedPluginLocator : IPluginLocator
    {
        private static Dictionary<PluginBase, Assembly> _plugins = null;
        private static object _lockObj = new object();

        private string[] _pluginNames;

        public ReferencedPluginLocator()
        {
            _pluginNames = new string[] { "PlugableMvc" };
        }

        public ReferencedPluginLocator(params string[] pluginAssemblyNamePrefixes)
        {
            _pluginNames = pluginAssemblyNamePrefixes;
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
            List<Assembly> candidateAssemblies = new List<Assembly>();

            foreach (var assemblyName in DependencyContext.Default.RuntimeLibraries)
            {
                if (isPossiblePlugin(assemblyName.Name))
                {
                    Assembly assembly = null;
                    assembly = Assembly.Load(new AssemblyName(assemblyName.Name));
                    candidateAssemblies.Add(assembly);
                }
            }

            foreach (var candidateAssembly in candidateAssemblies)
            {
                var pluginType = candidateAssembly
                    .ExportedTypes
                    .Where(x => !x.GetTypeInfo().IsAbstract)
                    .FirstOrDefault(x => typeof(PluginBase).IsAssignableFrom(x));

                if (pluginType == null)
                    continue;

                var instance = Activator.CreateInstance(pluginType) as PluginBase;

                if (_plugins == null)
                    _plugins = new Dictionary<PluginBase, Assembly>();

                _plugins.Add(instance, candidateAssembly);
            }
        }

        private bool isPossiblePlugin(string assemblyName)
        {
            foreach(var prefix in _pluginNames)
            {
                if (assemblyName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}

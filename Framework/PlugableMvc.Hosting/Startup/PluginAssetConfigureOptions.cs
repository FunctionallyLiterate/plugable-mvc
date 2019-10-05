using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using PlugableMvc.Plugins;
using System;
using System.Collections.Generic;

namespace PlugableMvc.Hosting.Startup
{
    public class PluginAssetConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IHostingEnvironment _environment;
        private readonly IPluginLocator _locator;
        public PluginAssetConfigureOptions(IHostingEnvironment environment, IPluginLocator locator)
        {
            _environment = environment;
            _locator = locator;
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {

            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();

            if (options.FileProvider == null && _environment.WebRootFileProvider == null)
                throw new InvalidOperationException("Missing FileProvider.");

            options.FileProvider = options.FileProvider ?? _environment.WebRootFileProvider;

            // Add plugin providers
            var providers = new List<IFileProvider> {
                options.FileProvider
            };

            foreach(var p in _locator.Locate())
                providers.Add(new ManifestEmbeddedFileProvider(p.Value, p.Key.StaticAssetFolderName));
            
            options.FileProvider = new CompositeFileProvider(providers);
        }
    }
}

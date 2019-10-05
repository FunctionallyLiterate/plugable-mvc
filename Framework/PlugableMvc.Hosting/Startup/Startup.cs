using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using PlugableMvc.Plugins;
using System;
using System.Linq;

namespace PlugableMvc.Hosting.Startup
{
    public static class Startup
    {
        public static IServiceCollection AddPluginLocator(this IServiceCollection services, IPluginLocator locator)
        {
            services.AddSingleton<IPluginLocator>(locator);
            return services;
        }

        public static IMvcBuilder AddPlugableMvc(this IMvcBuilder mvc)
        {
            var intermediateProvider = mvc.Services.BuildServiceProvider();
            var locator = intermediateProvider.GetRequiredService<IPluginLocator>();
            locator.Load();
            var plugins = locator.Locate();

           // mvc.Services.ConfigureOptions<PluginAssetConfigureOptions>();

            //add application parts
            mvc.ConfigureApplicationPartManager(manager =>
            {
                foreach (var p in plugins)
                {
                    var part = new AssemblyPart(p.Value);
                    manager.ApplicationParts.Add(part);
                }
            });

            //add MVC services
            foreach (var pluginAction in plugins.SelectMany(x => x.Key.ConfigureServiceActions).OrderByDescending(x => x.Priority))
                pluginAction.ConfigureServices(mvc.Services);

            return mvc;
        }

        public static IApplicationBuilder UsePlugableMvc(this IApplicationBuilder builder)
        {
            VerifyMvcIsRegistered(builder);
            var pluginLocator = (IPluginLocator)builder.ApplicationServices.GetRequiredService(typeof(IPluginLocator));
            var plugins = pluginLocator.Locate();

            foreach (var pluginAction in plugins.SelectMany(x => x.Key.ConfigureActions).OrderByDescending(x => x.Priority))
                pluginAction.Configure(builder);

            //adds static assets for plugins
            foreach (var p in plugins.Where(x => x.Key.UseStaticFiles))
            {
                builder.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new ManifestEmbeddedFileProvider(p.Value, p.Key.StaticAssetFolderName),
                    RequestPath = "/" + p.Key.StaticAssetRoutePrefix
                });
            }

            return builder;
        }

        private static void VerifyMvcIsRegistered(IApplicationBuilder app)
        {
            // Verify if AddMvc was done before calling UseMvc
            // We use the MvcMarkerService to make sure if all the services were added.
            if (app.ApplicationServices.GetService(typeof(MvcMarkerService)) == null)
            {
                throw new InvalidOperationException("You must call AddMvc in the ConfigureServices section");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlugableMvc.Events;
using PlugableMvc.Everything;
using PlugableMvc.Host.Events;
using PlugableMvc.Hosting.Plugins;
using PlugableMvc.Hosting.Startup;

namespace PlugableMvc.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                //.AddPluginLocator(new StaticPluginLocator(plugins =>
                //{
                //    plugins.Add(new EverythingPlugin());
                //}))
                .AddPluginLocator(new ReferencedPluginLocator())
                .AddMvc()
                .AddPlugableMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.RegisterEventHandler<TestHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UsePlugableMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

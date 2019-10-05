using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using PlugableMvc.Events;
using PlugableMvc.Host.Events;
using PlugableMvc.Host.Models;

namespace PlugableMvc.Host.Controllers
{
    public class HomeController : Controller
    {
        private IFileProvider f;
        private IEventBroadcaster _broadcaster;

        public HomeController(IHostingEnvironment env, IEventBroadcaster b)
        {
            f = env.WebRootFileProvider;
            _broadcaster = b;
        }
        public IActionResult Index()
        {
            _broadcaster.BroadcastAsync(TestHandler.TestEventName);

            return View(f);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

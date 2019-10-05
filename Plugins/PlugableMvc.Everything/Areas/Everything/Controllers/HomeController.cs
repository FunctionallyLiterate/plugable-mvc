using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlugableMvc.Plugins;

namespace PlugableMvc.Everything.Areas.Everything.Controllers
{
    [Area("Everything")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }      
    }
}

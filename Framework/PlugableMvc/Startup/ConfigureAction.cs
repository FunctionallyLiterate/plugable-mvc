using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlugableMvc.Startup
{
    public class ConfigureAction
    {
        public int Priority { get; set;  } = 1;
        public Action<IApplicationBuilder> Configure { get; set; }

    }
}

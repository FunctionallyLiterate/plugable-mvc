using Microsoft.AspNetCore.Builder;
using System;

namespace PlugableMvc.Startup
{
    public class ConfigureAction
    {
        public int Priority { get; set;  } = 1;
        public Action<IApplicationBuilder> Configure { get; set; }

    }
}

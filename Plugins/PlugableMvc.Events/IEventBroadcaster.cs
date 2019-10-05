using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlugableMvc.Events
{
    public interface IEventBroadcaster
    {
        Task BroadcastAsync(string name, params object[] args);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlugableMvc.Events
{
    public interface IEventHandler
    {
        string EventName { get; }
        Func<object[], Task> Handle { get; }
    }
}

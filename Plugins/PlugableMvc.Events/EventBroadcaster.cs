using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugableMvc.Events
{
    public class EventBroadcaster : IEventBroadcaster
    {
        private IEnumerable<IEventHandler> _handlers;

        public EventBroadcaster(IEnumerable<IEventHandler> handlers)
        {
            if (handlers.Any(x => x.EventName == null))
                throw new ArgumentException("One or more event handlers have a null EventName");

            _handlers = handlers;
        }

        public async Task BroadcastAsync(string name, params object[] args)
        {
            var handlers = _handlers.Where(x => x.EventName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            await Task.WhenAll(handlers.Select(x => x.Handle.Invoke(args)));
        }
    }
}

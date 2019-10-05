using PlugableMvc.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PlugableMvc.Host.Events
{
    public class TestHandler : IEventHandler
    {
        public static string TestEventName = "TestEvent";
        public string EventName => TestEventName;

        public Func<object[], Task> Handle => (args) =>
        {
            Trace.WriteLine(TestEventName + " handled");
            return Task.CompletedTask;
        };
    }
}

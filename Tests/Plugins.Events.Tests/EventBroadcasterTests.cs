using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PlugableMvc.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plugins.Events.Tests
{
    [TestClass]
    public class EventBroadcasterTests
    {
        [TestMethod]
        public async Task Broadcast_CanBroadcastEvent_ToSingleHandler()
        {
            object[] passedArgouments = null;
            var eventName = "Test.Event";
            var handler = new Mock<IEventHandler>();
            handler.Setup(x => x.EventName).Returns(eventName);
            handler.Setup(x => x.Handle)
                .Returns(args =>
                {
                    passedArgouments = args;
                    return Task.CompletedTask;
                });
            var broadcaster = new EventBroadcaster(new List<IEventHandler> { handler.Object });

            await broadcaster.BroadcastAsync(eventName, new object[] { "arg1" });

            Assert.IsNotNull(passedArgouments);
            Assert.AreEqual(passedArgouments.Count(), 1);
            Assert.AreEqual(passedArgouments[0], "arg1");
        }

        [TestMethod]
        public async Task Broadcast_CanBroadcastEvent_ToHandlerWithDifferentCaseEventName()
        {
            object[] passedArgouments = null;
            var eventName = "Test.Event";
            var upperName = eventName.ToUpper();
            var handler = new Mock<IEventHandler>();
            handler.Setup(x => x.EventName).Returns(upperName);
            handler.Setup(x => x.Handle)
                .Returns(args =>
                {
                    passedArgouments = args;
                    return Task.CompletedTask;
                });
            var broadcaster = new EventBroadcaster(new List<IEventHandler> { handler.Object });

            await broadcaster.BroadcastAsync(eventName, new object[] { "arg1" });

            Assert.IsNotNull(passedArgouments);
            Assert.AreEqual(passedArgouments.Count(), 1);
            Assert.AreEqual(passedArgouments[0], "arg1");
        }

        [TestMethod]
        public async Task Broadcast_CanBroadcastEvent_ToMultipleHandlers()
        {
            var invokedCt = 0;
            var eventName = "Test.Event";
            var handler1 = new Mock<IEventHandler>();
            handler1.Setup(x => x.EventName).Returns(eventName);
            handler1.Setup(x => x.Handle)
                .Returns(args =>
                {
                    invokedCt++;
                    return Task.CompletedTask;
                });

            var handler2 = new Mock<IEventHandler>();
            handler2.Setup(x => x.EventName).Returns(eventName);
            handler2.Setup(x => x.Handle)
                .Returns(args =>
                {
                    invokedCt++;
                    return Task.CompletedTask;
                });
            var broadcaster = new EventBroadcaster(new List<IEventHandler> { handler1.Object, handler2.Object });

            await broadcaster.BroadcastAsync(eventName, "testarg");

            Assert.AreEqual(invokedCt, 2);
        }

        [TestMethod]
        public async Task Broadcast_CanBeInjected()
        {
            var invokedCt = 0;
            var eventName = "Test.Event";
            var collection = new ServiceCollection();
            var handler1 = new Mock<IEventHandler>();
            handler1.Setup(x => x.EventName).Returns(eventName);
            handler1.Setup(x => x.Handle)
                .Returns(args =>
                {
                    invokedCt++;
                    return Task.CompletedTask;
                });

            collection.AddSingleton<IEventHandler>(handler1.Object);
            collection.AddTransient<IEventBroadcaster, EventBroadcaster>();
            var services = collection.BuildServiceProvider();

            var broadcaster = services.GetRequiredService<IEventBroadcaster>();
            await broadcaster.BroadcastAsync(eventName, "testarg");

            Assert.AreEqual(invokedCt, 1);
        }
    }
}

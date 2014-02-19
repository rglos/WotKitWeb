using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;

namespace LocalClientLibrary
{
    public class MyEventSource : EventSource
    {
        private static readonly Lazy<MyEventSource> Instance = new Lazy<MyEventSource>(() => new MyEventSource());

        public MyEventSource()
        {

        }

        public static MyEventSource Log { get { return Instance.Value; } }

        [Event(1)]
        public void Message(string message)
        {
            this.WriteEvent(1, message);
        }

        [Event(2, Level = EventLevel.Warning)]
        public void Warning(string message)
        {
            this.WriteEvent(2, message);
        }

        [Event(3, Level = EventLevel.Error)]
        public void Error(string message)
        {
            this.WriteEvent(3, message);
        }

        [Event(4, Level = EventLevel.Verbose)]
        public void DebugMessage(string message)
        {
            this.WriteEvent(4, message);
        }
    }
}

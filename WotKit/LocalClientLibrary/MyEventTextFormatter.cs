using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class MyEventTextFormatter : IEventTextFormatter
    {
        public void WriteEvent(Microsoft.Practices.EnterpriseLibrary.SemanticLogging.EventEntry eventEntry, System.IO.TextWriter writer)
        {
            writer.Write(eventEntry.Timestamp.ToString("g"));
            writer.Write(" - ");
            writer.WriteLine(eventEntry.Payload[0]);
        }
    }
}

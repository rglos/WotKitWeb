using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class MyConsoleColorMapper : IConsoleColorMapper
    {
        public ConsoleColor? Map(System.Diagnostics.Tracing.EventLevel eventLevel)
        {
            switch (eventLevel)
            {
                case EventLevel.Critical:
                    return ConsoleColor.White;
                case EventLevel.Error:
                    return ConsoleColor.DarkMagenta;
                case EventLevel.Warning:
                    return ConsoleColor.DarkYellow;
                case EventLevel.Verbose:
                    return ConsoleColor.Blue;
                default:
                    return null;
            }
        }
    }
}

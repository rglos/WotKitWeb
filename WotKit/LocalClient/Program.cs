using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Override default values to make the window abit bigger
            Console.SetWindowSize(120, 50);
            Console.SetBufferSize(120, 3000);

            //var listener1 = new ObservableEventListener();
            //listener1.EnableEvents(LocalClientLibrary.MyEventSource.Log, System.Diagnostics.Tracing.EventLevel.LogAlways);
            //listener1.LogToConsole();

            var consoleListener = ConsoleLog.CreateListener(new LocalClientLibrary.MyEventTextFormatter());
            consoleListener.EnableEvents(LocalClientLibrary.MyEventSource.Log, System.Diagnostics.Tracing.EventLevel.Informational);

            var flatFileListener = RollingFlatFileLog.CreateListener("RollingFlatFile.log", 0, "yyyy", Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks.RollFileExistsBehavior.Increment, Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks.RollInterval.Day);
            flatFileListener.EnableEvents(LocalClientLibrary.MyEventSource.Log, System.Diagnostics.Tracing.EventLevel.LogAlways);

            //LocalClientLibrary.MyEventSource.Log.Message("Test!");
            //LocalClientLibrary.MyEventSource.Log.Warning("Warning!");
            //LocalClientLibrary.MyEventSource.Log.Error("Error!");

            var applicationSettings = new LocalClientLibrary.ApplicationSettings();

            var battleResultsWatcher = new LocalClientLibrary.BattleResultsWatcher(applicationSettings);

            var dossierWatcher = new LocalClientLibrary.DossierWatcher(applicationSettings);

            // Silly way to keep this running for now - later we can make this a desktop app running in the systray or something
            //Console.WriteLine("Press [ANY] key to quit...");
            Console.ReadKey();
        }
    }
}

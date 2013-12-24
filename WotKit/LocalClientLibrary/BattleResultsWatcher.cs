using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class BattleResultsWatcher
    {
        public BattleResultsWatcher(ApplicationSettings applicationSettings)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = applicationSettings.WorldOfTanksBattleResultsFolder;
            watcher.IncludeSubdirectories = true;
            watcher.Filter = "*.dat";
            watcher.Changed += Changed;
            watcher.EnableRaisingEvents = true;
        }

        void Changed(object sender, FileSystemEventArgs e)
        {
            var message = string.Format("BattleResultsWatcher: Changed; e.Name: {0}; e.ChangeType: {1}; e.Fullpath: {2}", e.Name, e.ChangeType, e.FullPath);
            LocalClientLibrary.MyEventSource.Log.Message(message);

            LocalClientLibrary.MyEventSource.Log.Message("Converting Battle Result to JSON");
            var json = ConvertBattleResultToJSONService.Convert(e.FullPath);
            LocalClientLibrary.MyEventSource.Log.Message("Convert complete");

            LocalClientLibrary.MyEventSource.Log.DebugMessage(json);

            // Parse Json
            // server
            // user
            // tank
            // stats

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class DossierWatcher
    {
        public DossierWatcher(ApplicationSettings applicationSettings)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = applicationSettings.WorldOfTanksDossierFolder;
            watcher.Filter = "*.dat";
            watcher.Changed += Changed;
            watcher.EnableRaisingEvents = true;
        }

        void Changed(object sender, FileSystemEventArgs e)
        {
            var message = string.Format("DossierWatcher: Changed; e.Name: {0}", e.Name);
            //var message = string.Format("DossierWatcher: Changed; e.Name: {0}; e.ChangeType: {1}; e.Fullpath: {2}", e.Name, e.ChangeType, e.FullPath);
            LocalClientLibrary.MyEventSource.Log.Message(message);
        }
    }
}

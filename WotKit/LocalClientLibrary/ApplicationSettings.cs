using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            var applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            LocalClientLibrary.MyEventSource.Log.Message(applicationDataFolder);

            var worldOfTanksFolder = System.IO.Path.Combine(applicationDataFolder, @"wargaming.net\WorldOfTanks");
            LocalClientLibrary.MyEventSource.Log.Message(worldOfTanksFolder);

            var worldOfTanksBattleResultsFolder = System.IO.Path.Combine(worldOfTanksFolder, @"battle_results");
            LocalClientLibrary.MyEventSource.Log.Message(worldOfTanksBattleResultsFolder);
            this.WorldOfTanksBattleResultsFolder = worldOfTanksBattleResultsFolder;

            var worldOfTanksDossierCacheFolder = System.IO.Path.Combine(worldOfTanksFolder, @"dossier_cache");
            LocalClientLibrary.MyEventSource.Log.Message(worldOfTanksDossierCacheFolder);
            this.WorldOfTanksDossierFolder = worldOfTanksDossierCacheFolder;
        }

        public string WorldOfTanksBattleResultsFolder { get; set; }
        public string WorldOfTanksDossierFolder { get; set; }
        public string WorldOfTanksReplaysFolder { get; set; }
    }
}

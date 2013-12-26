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
        public BattleResultsWatcher()
        {

        }

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

            ProcessFile(e.FullPath);
        }

        public void ProcessFile(string path)
        {
            LocalClientLibrary.MyEventSource.Log.Message("Converting Battle Result to JSON");
            var json = ConvertBattleResultToJSONService.Convert(path);
            LocalClientLibrary.MyEventSource.Log.Message("Convert complete");

            LocalClientLibrary.MyEventSource.Log.DebugMessage(json);

            // Parse Json
            // server
            // user
            // tank
            // stats
            var player = BattleResultsParser.ParsePlayer(json);
            LocalClientLibrary.MyEventSource.Log.Message(string.Format("Player: parsed; AccountDBID: {0}, Name: {1}", player.AccountDBID, player.Name));
            var serverPlayer = WebApiService.PostPlayer(player).Result;
            LocalClientLibrary.MyEventSource.Log.Message(string.Format("WebApiService.PostPlayer: posted; PlayerId: {0}", serverPlayer.PlayerId));

            var battle = BattleResultsParser.ParseBattle(json);
            LocalClientLibrary.MyEventSource.Log.Message(string.Format("BattleResultsParser.ParseBattle: finished; ArenaUniqueId: {0}", battle.ArenaUniqueId));
            var serverBattle = WebApiService.PostBattle(battle).Result;
            LocalClientLibrary.MyEventSource.Log.Message(string.Format("WebApiService.PostBattle: posted; BattleId: {0}", serverBattle.BattleId));

            var playerBattle = BattleResultsParser.ParsePlayerBattle(json, serverPlayer, serverBattle);
            LocalClientLibrary.MyEventSource.Log.Message(string.Format("BattleResultsParser.ParsePlayerBattle: finished; TankName: {0}", playerBattle.TankName));
            var serverPlayerBattle = WebApiService.PostPlayerBattle(playerBattle).Result;
            LocalClientLibrary.MyEventSource.Log.Message(string.Format("WebApiService.PostPlayerBattle: posted; DamageDealt: {0}", serverPlayerBattle.DamageDealt));

            // TODO: Notify server to refresh recent battles for this player
        }
    }
}

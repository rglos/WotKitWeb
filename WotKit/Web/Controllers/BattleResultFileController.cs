using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Web.Domain.Models;
using Web.Services;

namespace Web.Controllers
{
    public class BattleResultFileController : ApiController
    {
        private WotKitEntities db = new WotKitEntities();

        public async Task<HttpResponseMessage> PostBattleResultFileData()
        {
            // Verify that this is an HTML form file upload request
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // TODO: Use dependency injection to MapPath - http://stackoverflow.com/a/19563226
            //string root = HttpContext.Current.Server.MapPath("~/App_Data");
            string root = @"D:\Data\WoT\Temp";
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data
                await Request.Content.ReadAsMultipartAsync(provider);

                // Process the file(s)
                foreach (MultipartFileData file in provider.FileData)
                {
                    System.Diagnostics.Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    System.Diagnostics.Trace.WriteLine("Server file path: " + file.LocalFileName);

                    var newFilePath = Path.Combine(Path.GetDirectoryName(file.LocalFileName), file.Headers.ContentDisposition.FileName);
                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }
                    File.Move(file.LocalFileName, newFilePath);

                    var json = ConvertBattleResultToJSONService.Convert(newFilePath);
                    var player = BattleResultsParser.ParsePlayer(json);
                    var serverPlayer = PostPlayer(player);
                    var battle = BattleResultsParser.ParseBattle(json);
                    var serverBattle = PostBattle(battle);
                    var playerBattle = BattleResultsParser.ParsePlayerBattle(json, serverPlayer, serverBattle);
                    var serverPlayerBattle = PostPlayerBattle(playerBattle);
                }

                // Process any additional form data
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("{0}: {1}", key, val));
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        private Player PostPlayer(Player player)
        {
            var serverPlayer = db.Players.Where(x => x.AccountDBID == player.AccountDBID).SingleOrDefault();
            if (serverPlayer == null)
            {
                db.Players.Add(player);
                db.SaveChanges();
                serverPlayer = player;
            }
            return serverPlayer;
        }

        private Battle PostBattle(Battle battle)
        {
            var serverBattle = db.Battles.Where(x => x.ArenaUniqueId == battle.ArenaUniqueId).SingleOrDefault();
            if (serverBattle == null)
            {
                db.Battles.Add(battle);
                db.SaveChanges();
                serverBattle = battle;
            }
            return serverBattle;
        }

        private PlayerBattle PostPlayerBattle(PlayerBattle playerBattle)
        {
            var serverPlayerBattle = db.PlayerBattles.Where(x => x.BattleId == playerBattle.BattleId && x.PlayerId == playerBattle.PlayerId).SingleOrDefault();
            if (serverPlayerBattle == null)
            {
                db.PlayerBattles.Add(playerBattle);
                db.SaveChanges();
                serverPlayerBattle = playerBattle;
            }
            return serverPlayerBattle;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Domain.Models;
using Web.Models;

namespace Web.Controllers
{
    public class BattlesController : Controller
    {
        private WotKitEntities db = new WotKitEntities();

        public ActionResult Index()
        {
            var model = new BattlesIndexModel();

            var query = from x in db.PlayerBattles
                        group x.Player by x.Player.Name into g
                        select new { Name = g.Key, Count = g.Count() };

            foreach (var item in query)
            {
                var battlesIndexRowModel = new BattlesIndexRowModel() { Player = item.Name, RecentBattlesCount = item.Count };
                model.Rows.Add(battlesIndexRowModel);
            }

            return View(model);
        }
        
        public ActionResult Recent(string id)
        {
            var model = new RecentBattlesModel();

            var player = db.Players.Where(x => x.Name == id).SingleOrDefault();
            if (player == null)
            {
                model.DisplayBattles = false;
                model.Message = string.Format("Player '{0}' not found", id);
                return View(model);
            }

            if (TempData["Message"] != null)
            {
                model.Message = TempData["Message"].ToString();
            }
            
            model.AccountDBID = player.AccountDBID;
            model.Name = player.Name;

            var playerBattles = from x in db.PlayerBattles
                                where x.PlayerId == player.PlayerId
                                //join y in db.WN8ExpectedTankValues on x.TankName equals y.
                                orderby x.Battle.ArenaCreateTime descending
                                select new RecentBattleModel()
                                {
                                    Tank = x.TankName,
                                    Map = x.Battle.ArenaTypeName,
                                    DamageDealt = x.DamageDealt,
                                    DamageReceived = x.DamageReceived,
                                    BattleDate = x.Battle.ArenaCreateTime,
                                    autoEquipCost = x.autoEquipCost,
                                    autoLoadCost = x.autoLoadCost,
                                    autoRepairCost = x.autoRepairCost,
                                    credits = x.credits,
                                    eventFreeXP = x.eventFreeXP,
                                    eventTMenXP = x.eventTMenXP,
                                    eventXP = x.eventXP,
                                    freeXP = x.freeXP,
                                    tmenXP = x.tmenXP,
                                    XP = x.XP,
                                    XPPenalty = x.XPPenalty,
                                    NetCredits = (x.credits - x.autoRepairCost - x.autoLoadCost - x.autoEquipCost),
                                    Won = x.won,
                                    WinnerTeam = x.Battle.WinnerTeam,
                                    Kills = x.kills,
                                    Spotted = x.spotted,
                                    CapturePoints = x.capturePoints,
                                    DroppedCapturePoints = x.droppedCapturePoints
                                };

            model.Battles = playerBattles.ToList();

            foreach (var battle in model.Battles)
            {
                // Draw - how do we know if it is a draw?
                if (battle.Won)
                {
                    battle.Status = "Victory";
                    battle.StatusClass = "success";
                    battle.StatusTextClass = "text-success";
                }
                else if (battle.WinnerTeam == 0)
                {
                    battle.Status = "Draw";
                    battle.StatusClass = "warning";
                    battle.StatusTextClass = "text-warning";
                }
                else
                {
                    battle.Status = "Defeat";
                    battle.StatusClass = "danger";
                    battle.StatusTextClass = "text-danger";
                }

                if (battle.NetCredits < 0)
                {
                    battle.NetCreditsClass = "text-danger";
                }
            }

            model.Summary.NetCredits = model.Battles.Sum(x => x.NetCredits);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult StartNewSession(string name)
        {
            var playerBattles = db.PlayerBattles.Where(x => x.Player.Name == name);
            db.PlayerBattles.RemoveRange(playerBattles);
            db.SaveChanges();

            var battles = db.Battles.Where(x => x.PlayerBattles.Count == 0);
            db.Battles.RemoveRange(battles);
            db.SaveChanges();

            TempData["Message"] = "Session successfully reset";
            return RedirectToAction("Recent", "Battles", new { id = name });
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
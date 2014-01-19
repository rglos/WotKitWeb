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
                                    DroppedCapturePoints = x.droppedCapturePoints,
                                    TankId = x.typeCompDescr
                                };

            model.Battles = playerBattles.ToList();

            var tanks = db.Tanks.ToList();

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

                // Calculating WN8
                // Step 1
                var tank = tanks.Where(x => x.TankId == battle.TankId).Single();
                double rDamage = (double)battle.DamageDealt / tank.ExpectedDamage;
                double rSpot = (double)battle.Spotted / tank.ExpectedSpot;
                double rFrag = (double)battle.Kills / tank.ExpectedFrag;
                double rDef = (double)battle.DroppedCapturePoints / tank.ExpectedDefense;
                double rWin = battle.Won ? tank.ExpectedWin : 0;
                // Step 2
                var rWINc =    Math.Max(0, (rWin    - 0.71) / (1 - 0.71));
                var rDAMAGEc = Math.Max(0, (rDamage - 0.22) / (1 - 0.22));
                var rFRAGc = Math.Max(0, Math.Min(rDAMAGEc + 0.2, (rFrag - 0.12) / (1 - 0.12)));
                var rSPOTc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rSpot - 0.38) / (1 - 0.38)));
                var rDEFc = Math.Max(0, Math.Min(rDAMAGEc + 0.1, (rDef - 0.10) / (1 - 0.10)));
                // Step 3
                var wn8 = (980 * rDAMAGEc) + (210 * rDAMAGEc * rFRAGc) + (155 * rFRAGc * rSPOTc) + (75 * rDEFc * rFRAGc) + (145 * Math.Min(1.8, rWINc));

                battle.WN8 = wn8;
            }

            // Summary
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
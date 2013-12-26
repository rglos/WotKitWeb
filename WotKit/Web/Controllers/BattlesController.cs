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
            return View();
        }
        
        public ActionResult Recent(string id)
        {
            var model = new RecentBattlesModel();

            var player = db.Players.Where(x => x.Name == id).SingleOrDefault();
            if (player == null)
            {
                ViewBag.Message = string.Format("Player '{0}' not found", id);
                return View(model);
            }
            
            model.AccountDBID = player.AccountDBID;
            model.Name = player.Name;

            var playerBattles = from x in db.PlayerBattles
                                where x.PlayerId == player.PlayerId
                                orderby x.Battle.ArenaCreateTime descending
                                select new RecentBattleModel()
                                {
                                    Tank = x.TankName,
                                    DamageDealt = x.DamageDealt,
                                    DamageReceived = x.DamageReceived,
                                    BattleDate = x.Battle.ArenaCreateTime
                                };

            model.Battles = playerBattles.ToList();
            
            return View(model);
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
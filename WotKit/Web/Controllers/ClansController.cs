using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Domain.Models;
using Web.Models;

namespace Web.Controllers
{
    public class ClansController : Controller
    {
        public ActionResult Index()
        {
            var model = new List<ClanModel>();

            using (WotKitEntities db = new WotKitEntities())
            {
                var query = from x in db.Clans
                            select new ClanModel
                            {
                                ClanId = x.ClanId,
                                Abbreviation = x.Abbreviation,
                                Name = x.Name,
                                EmblemBWTank = x.EmblemBWTank,
                                EmblemLarge = x.EmblemLarge,
                                EmblemMedium = x.EmblemMedium,
                                EmblemSmall = x.EmblemSmall,
                                CreatedAtDate = x.CreatedAtDate,
                                UpdatedAtDate = x.UpdatedAtDate
                            };

                model = query.ToList();
            }

            // TODO: Implement this, http://stackoverflow.com/a/19301623/16008, so we can set this on the models in the EF
            foreach (var item in model)
            {
                item.CreatedAtDate = DateTime.SpecifyKind(item.CreatedAtDate, DateTimeKind.Utc);
                item.UpdatedAtDate = DateTime.SpecifyKind(item.UpdatedAtDate, DateTimeKind.Utc);
            }

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new ClanDetailModel();

            using (WotKitEntities db = new WotKitEntities())
            {
                var clan = db.Clans.Where(x => x.ClanId == id).Single();

                model.Name = clan.Name;
                model.Abbreviation = clan.Abbreviation;
                model.ClanId = clan.ClanId;
                model.EmblemSmall = clan.EmblemSmall;

                var maxAsOfDate = db.ClanDetails.Where(x => x.ClanId == id).Max(x => x.AsOfDate);
                model.AsOfDate = maxAsOfDate;

                var members = from cd in db.ClanDetails
                              where cd.ClanId == id && cd.AsOfDate == maxAsOfDate
                              select new ClanMemberDetailModel
                              {
                                  Name = cd.Player.Name,
                                  Position = cd.Rolei18n,
                                  AllBattles = cd.AllBattles,
                                  ClanBattles = cd.ClanBattles,
                                  CompanyBattles = cd.CompanyBattles
                              };
                model.ClanMembers = members.ToList();   
            }

            return View(model);
        }
    }
}
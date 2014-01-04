using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Domain.Models;
using Web.Models;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private WotKitEntities db = new WotKitEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WN8ExpectedTankValues()
        {
            var model = new WN8ExpectedTankValuesModel();

            var query = from x in db.WN8ExpectedTankValues
                        select new WN8ExpectedTankValuesRowModel
                        {
                            Class = x.Class,
                            Damage = x.Damage,
                            Defense = x.Defense,
                            Frag = x.Frag,
                            IDNum = x.IDNum,
                            Name = x.Name,
                            Nation = x.Nation,
                            Spot = x.Spot,
                            Tier = x.Tier,
                            Win = x.Win
                        };

            model.Rows.AddRange(query.ToList());

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
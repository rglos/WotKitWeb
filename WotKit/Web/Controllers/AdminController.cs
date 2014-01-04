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

        public ActionResult Tanks()
        {
            var model = new TanksIndexModel();

            var query = from x in db.Tanks
                        select new TanksIndexRowModel
                        {
                            Level = x.Level,
                            Name = x.Namei18n,
                            Nation = x.Nationi18n,
                            TankId = x.TankId,
                            TankType = x.TankType
                        };
            model.Tanks.AddRange(query.ToList());

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
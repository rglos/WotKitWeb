using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Domain.Models;

namespace Web.Controllers
{
    public class TanksController : Controller
    {
        private WotKitEntities db = new WotKitEntities();

        // GET: /Tanks/
        public ActionResult Index()
        {
            return View(db.Tanks.ToList());
        }

        // GET: /Tanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tank tank = db.Tanks.Find(id);
            if (tank == null)
            {
                return HttpNotFound();
            }
            return View(tank);
        }

        //// GET: /Tanks/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /Tanks/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="TankId,Nation,Name,Level,IsPremium,Namei18n,Nationi18n,TankType,ExpectedFrag,ExpectedDamage,ExpectedSpot,ExpectedDefense,ExpectedWin")] Tank tank)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tanks.Add(tank);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tank);
        //}

        //// GET: /Tanks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tank tank = db.Tanks.Find(id);
        //    if (tank == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tank);
        //}

        //// POST: /Tanks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="TankId,Nation,Name,Level,IsPremium,Namei18n,Nationi18n,TankType,ExpectedFrag,ExpectedDamage,ExpectedSpot,ExpectedDefense,ExpectedWin")] Tank tank)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tank).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tank);
        //}

        //// GET: /Tanks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tank tank = db.Tanks.Find(id);
        //    if (tank == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tank);
        //}

        //// POST: /Tanks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tank tank = db.Tanks.Find(id);
        //    db.Tanks.Remove(tank);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

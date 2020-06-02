using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeliveryServiceWebDBServer.Models;

namespace DeliveryServiceWebDBServer.Controllers
{
    public class TempController : Controller
    {
        private ModelDBContainer db = new ModelDBContainer();

        // GET: Temp
        public ActionResult Index()
        {
            var packages = db.Packages.Include(p => p.PersonFrom).Include(p => p.PersonTo).Include(p => p.Tariff);
            return View(packages.ToList());
        }

        // GET: Temp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Temp/Create
        public ActionResult Create()
        {
            ViewBag.PersonIdFrom = new SelectList(db.Persons, "Id", "Name");
            ViewBag.PersonIdTo = new SelectList(db.Persons, "Id", "Name");
            ViewBag.TariffId = new SelectList(db.Tariffs, "Id", "Name");
            return View();
        }

        // POST: Temp/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonIdFrom,PersonIdTo,TariffId,Description,Weight,Length,Width,Height,NumberOfPackages,Cost,DeclaredValue")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Packages.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonIdFrom = new SelectList(db.Persons, "Id", "Name", package.PersonIdFrom);
            ViewBag.PersonIdTo = new SelectList(db.Persons, "Id", "Name", package.PersonIdTo);
            ViewBag.TariffId = new SelectList(db.Tariffs, "Id", "Name", package.TariffId);
            return View(package);
        }

        // GET: Temp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonIdFrom = new SelectList(db.Persons, "Id", "Name", package.PersonIdFrom);
            ViewBag.PersonIdTo = new SelectList(db.Persons, "Id", "Name", package.PersonIdTo);
            ViewBag.TariffId = new SelectList(db.Tariffs, "Id", "Name", package.TariffId);
            return View(package);
        }

        // POST: Temp/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonIdFrom,PersonIdTo,TariffId,Description,Weight,Length,Width,Height,NumberOfPackages,Cost,DeclaredValue")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonIdFrom = new SelectList(db.Persons, "Id", "Name", package.PersonIdFrom);
            ViewBag.PersonIdTo = new SelectList(db.Persons, "Id", "Name", package.PersonIdTo);
            ViewBag.TariffId = new SelectList(db.Tariffs, "Id", "Name", package.TariffId);
            return View(package);
        }

        // GET: Temp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Temp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
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

﻿using System;
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
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private ModelDBContainer db = new ModelDBContainer();
        private List<string> Statuses = Properties.Settings.Default.Statuses.Cast<string>().ToList();
        /*
           0 <string>Зарегистрировано</string> 
           1 <string>Ожидается отправка</string>
           2 <string>Отправлено в пункт назначения</string>
           3 <string>Прибыло в пункт назначения</string>
           4 <string>Доставлено в пункт выдачи</string>
           5 <string>Передано курьеру</string>
           6 <string>Доставлено</string>
           7 <string>Будет возвращено отправителю</string>
           8 <string>Прибыло для возврата отправителю</string>
           9 <string>Возвращено отправителю</string>
           10 <string>Отменено</string>
             */
        #region categories
        // GET: Admin
        public ActionResult Index()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            IEnumerable<Package> packages;
            if (centre == null) packages = db.Packages.Include(p => p.PersonFrom).Include(p => p.PersonTo).Include(p => p.Tariff);
            else packages = db.Packages.Where(a => a.PersonFrom.CentreId == centre.Id || a.PersonFrom.CityId == centre.CityId);
            PackageListViewModel list = new PackageListViewModel(packages, "Все", centre);
            return View(list);
        }
        public ActionResult Category1()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[0], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "Зарегистрированные", centre);
            return View("Index",list);
        }
        public ActionResult Category2()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[1], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "Ожидают отправки", centre);
            return View("Index", list);
        }
        public ActionResult Category3()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[2], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "В пути", centre);
            return View("Index", list);
        }
        public ActionResult Category4()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[3], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "Ожидают принятия", centre);
            return View("Index", list);
        }
        public ActionResult Category5()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[5], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "У курьеров", centre);
            return View("Index", list);
        }
        public ActionResult Category6()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[4], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "В пункте выдачи", centre);
            return View("Index", list);
        }
        public ActionResult Category7()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[6], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "Доставленные", centre);
            return View("Index", list);
        }
        public ActionResult Category8()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[7], centre);
            suitablePackages.AddRange(GetSuitablePackages(Statuses[8], centre));
            suitablePackages.AddRange(GetSuitablePackages(Statuses[9], centre));
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "Возвраты", centre);
            return View("Index", list);
        }
        public ActionResult Category9()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(Statuses[10], centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "Отмененные", centre);
            return View("Index", list);
        }
        public ActionResult Category10()
        {
            DistributionCentre centre = db.Users.FirstOrDefault(a => a.Login == User.Identity.Name).AccountReferences.FirstOrDefault()?.DistributionCentre;
            List<Package> suitablePackages = GetSuitablePackages(null, centre);
            PackageListViewModel list = new PackageListViewModel(suitablePackages, "Без статуса", centre);
            return View("Index", list);
        }

        private List<Package> GetSuitablePackages(string status, DistributionCentre centre)
        {
            IEnumerable<Package> packages;
            if (centre == null) packages = db.Packages.Include(p => p.PersonFrom).Include(p => p.PersonTo).Include(p => p.Tariff);
            else packages = db.Packages.Where(a => a.PersonFrom.CentreId == centre.Id || a.PersonFrom.CityId == centre.CityId);
            List<Package> suitablePackages = new List<Package>();
            foreach (Package item in packages)
            {
                var maxDate = item.Records.Select(a => a.DateAndTime).Max();
                if (item.Records.Where(a => a.DateAndTime == maxDate).Single().Status == status) suitablePackages.Add(item);
            }
            return suitablePackages;
        }

        public ActionResult CurrentCentre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DistributionCentre centre = db.DistributionCentres.Find(id);
            if (centre == null)
            {
                return HttpNotFound();
            }
            return View(centre);
        }
        #endregion

        // SEARCH
        // [HttpPost]
        public ActionResult Search(SearchIdModel searchId)
        {
            IEnumerable<Package> packages = db.Packages;
            List<int> ids = packages.Where(a => Helper.Check(a.Id.ToString(), searchId.Id.ToString())).Select(a => a.Id).ToList();
            if (ids.Count() == 0)
            {
                TempData["alertMessage"] = "Отправления с таким номером не найдены";
                return RedirectToAction("Index", "User");
            }
            else if (ids.Count() == 1) return RedirectToAction("Details", "Admin", new { id = ids.First() });
            ViewBag.suitedPackages = ids;
            return View("~/Views/Home/Search.cshtml");
        }

        // GET: Admin/Details/5
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

            List<IdValueModel> centres = new List<IdValueModel>();
            centres.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (DistributionCentre p in db.DistributionCentres)
            {
                string str1 = p.Address + ", " + p.City.NameCity + ", " + p.City.Region.NameRegion + ", " + p.City.Region.Country.NameCountry;
                centres.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.CentreId = new SelectList(centres, "Id", "Value", null);
            ViewBag.Status = new SelectList(Statuses);
            return View(package);
        }
        public ActionResult DeleteRecord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            int PackageId = record.Package.Id;
            db.Records.Remove(record);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = PackageId });
        }
        public ActionResult AddRecord(int PackageId, string Status, int? CentreId)
        {
            db.Records.Add(new Record { PackageId = PackageId, Status = Status, CentreId = CentreId, DateAndTime = DateTime.Now });
            db.SaveChanges();

            return RedirectToAction("Details", new { id = PackageId });
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.PersonIdFrom = new SelectList(db.Persons, "Id", "Name");
            ViewBag.PersonIdTo = new SelectList(db.Persons, "Id", "Name");
            ViewBag.TariffId = new SelectList(db.Tariffs, "Id", "Name");
            return View();
        }

        // POST: Admin/Create
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
        private void InitSelectLists()
        {
            List<IdValueModel> centres = new List<IdValueModel>();
            centres.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (DistributionCentre p in db.DistributionCentres)
            {
                centres.Add(new IdValueModel { Id = p.Id, Value = Helper.GetCentreString(p) });
            }
            List<IdValueModel> cities = new List<IdValueModel>();
            cities.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (City p in db.Cities)
            {
                string str1 = p.NameCity + ", " + p.Region.NameRegion + ", " + p.Region.Country.NameCountry;
                cities.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            List<IdValueModel> tariffs = new List<IdValueModel>();
            tariffs.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (Tariff p in db.Tariffs)
            {
                tariffs.Add(new IdValueModel { Id = p.Id, Value = p.Name });
            }
            Dictionary<string,string> descriptions = new Dictionary<string, string>();
            foreach (string p in Properties.Settings.Default.Descriptions)
            {
                descriptions.Add(p,p);
            }
            ViewBag.CentreId = new SelectList(centres, "Id", "Value", null);
            ViewBag.CityId = new SelectList(cities, "Id", "Value", null);
            ViewBag.TariffId = new SelectList(tariffs, "Id", "Value", null);
            ViewBag.Description = new SelectList(descriptions, "Key", "Value", null);
        }
        // GET: User/Create
        public ActionResult CreateStep1()
        {
            InitSelectLists();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStep1([Bind(Include = "Id,Name,MiddleName,Surname,Company,Phone,Mail,Index,CityId,Address,CentreId,InformingSMS,InformingMail")] Person person)
        {
            if (ModelState.IsValid)
            {
                Person p = db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("CreateStep2", new { personIdFrom = p.Id });
            }
            InitSelectLists();
            return View(person);
        }

        // GET: User/Create
        public ActionResult CreateStep2(int personIdFrom)
        {
            ToWithFromIdModel temp = new ToWithFromIdModel { FromId = personIdFrom };
            InitSelectLists();
            return View(temp);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStep2([Bind(Include = "FromId,Name,MiddleName,Surname,Company,Phone,Mail,Index,CityId,Address,CentreId,InformingSMS,InformingMail")] ToWithFromIdModel temp)
        {
            if (ModelState.IsValid)
            {
                int fromId = temp.FromId;
                Person personTo = db.Persons.Add(new Person
                {
                    Name = temp.Name,
                    MiddleName = temp.MiddleName,
                    Surname = temp.Surname,
                    Company = temp.Company,
                    Phone = temp.Phone,
                    Mail = temp.Mail,
                    Index = temp.Index,
                    CityId = temp.CityId,
                    Address = temp.Address,
                    CentreId = temp.CentreId,
                    InformingSMS = temp.InformingSMS,
                    InformingMail = temp.InformingMail,
                });
                db.SaveChanges();
                int toId = personTo.Id;
                return RedirectToAction("CreateStep3", new { personIdFrom = fromId, personIdTo = toId });
            }
            InitSelectLists();
            return View(temp);
        }

        public ActionResult CreateStep3(int personIdFrom, int personIdTo)
        {
            Package temp = new Package { PersonIdFrom = personIdFrom, PersonIdTo = personIdTo };
            InitSelectLists();
            return View(temp);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStep3([Bind(Include = "PersonIdFrom,PersonIdTo,TariffId,Description,Weight,Length,Width,Height,NumberOfPackages,Cost,DeclaredValue")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Packages.Add(package);
                db.SaveChanges();
                db.Records.Add(new Record { PackageId = package.Id, DateAndTime = DateTime.Now, Status = Statuses[0] });
                db.SaveChanges();
                TempData["alertMessage"] = "Отправление успешно зарегистрировано.";
                return RedirectToAction("Details", new { id = package.Id });
            }
            InitSelectLists();
            return View(package);
        }

        // GET: Admin/Edit/5
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
            InitSelectLists();
            return View(package);
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
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

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Constructor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TableSelect(string[] table)
        {
            //var allbooks = db.Books.Where(a => a.Author.Contains(name)).ToList();
            //if (allbooks.Count <= 0)
            //{
            //    return HttpNotFound();
            //}
            return PartialView("_Constructor");
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
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
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private ModelDBContainer db = new ModelDBContainer();
        private List<string> Statuses = Properties.Settings.Default.Statuses.Cast<string>().ToList();

        // GET: User
        public ActionResult Index()
        {
            IEnumerable<Package> packages = db.UserPackages.Where(a => a.User.Login == User.Identity.Name).Select(a => a.Package);
            PackageListViewModel list = new PackageListViewModel(packages, null, null);
            ViewBag.suitedPackages = null;
            ViewBag.wasSearch = false;
            return View(list);
        }

        // SEARCH
        // [HttpPost]
        public ActionResult Search(SearchIdModel searchId)
        {
            IEnumerable<Package> packages = db.UserPackages.Where(a => a.User.Login == User.Identity.Name).Select(a => a.Package);
            List<int> ids = packages.Where(a => Helper.Check(a.Id.ToString(), searchId.Id.ToString())).Select(a => a.Id).ToList();
            if (ids.Count() == 0)
            {
                TempData["alertMessage"] = "Отправления с таким номером не найдены";
                return RedirectToAction("Index", "User");
            }
            else if (ids.Count() == 1) return RedirectToAction("Details", "User", new { id = ids.First() });
            ViewBag.suitedPackages = ids;
            return View("~/Home/Search");
        }

        // GET: User/Details/5
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

        private void InitSelectLists()
        {
            IEnumerable<Person> persons = db.AccountReferences.Where(a => a.User.Login == User.Identity.Name).Select(a => a.Person);
            List<IdValueModel> savedPersons = new List<IdValueModel>();
            foreach (Person p in persons)
            {
                string str1 = "";
                if (p.Name != null) { str1 += p.Name + " "; }
                if (p.MiddleName != null) { str1 += p.MiddleName + " "; }
                if (p.Surname != null) { str1 += p.Surname + ", "; }
                if (p.Company != null) { str1 += p.Company + ", "; }
                if (p.CityId != null)
                {
                    if (p.City.Region.Country.NameCountry != null) { str1 += p.City.Region.Country.NameCountry + ", "; }
                    if (p.City.Region.NameRegion != null) { str1 += p.City.Region.NameRegion + ", "; }
                    if (p.City.NameCity != null) { str1 += p.City.NameCity + ", "; }
                    if (p.Address != null) { str1 += p.Address + " "; }
                }
                if (p.CentreId != null)
                {
                    str1 += "Пункт выдачи: ";
                    if (p.DistributionCentre.City.Region.Country != null) { str1 += p.DistributionCentre.City.Region.Country.NameCountry + ", "; }
                    if (p.DistributionCentre.City.Region != null) { str1 += p.DistributionCentre.City.Region.NameRegion + ", "; }
                    if (p.DistributionCentre.City != null) { str1 += p.DistributionCentre.City.NameCity + ", "; }
                    if (p.DistributionCentre != null) { str1 += p.DistributionCentre.Address + " "; }
                }
                if (p.InformingMail) str1 += "Оповещения по почте ";
                if (p.InformingSMS) str1 += "Оповещения по SMS";

                savedPersons.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            List<IdValueModel> centres = new List<IdValueModel>();
            centres.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (DistributionCentre p in db.DistributionCentres)
            {
                string str1 = p.Address + ", " + p.City.NameCity + ", " + p.City.Region.NameRegion + ", " + p.City.Region.Country.NameCountry;
                centres.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            List<IdValueModel> cities = new List<IdValueModel>();
            cities.Add(new IdValueModel { Id = null, Value = "(Не выбран)" });
            foreach (City p in db.Cities)
            {
                string str1 = p.NameCity + ", " + p.Region.NameRegion + ", " + p.Region.Country.NameCountry;
                cities.Add(new IdValueModel { Id = p.Id, Value = str1 });
            }
            ViewBag.SavedPerson = new SelectList(savedPersons, "Id", "Value", null);
            ViewBag.CentreId = new SelectList(centres, "Id", "Value", null);
            ViewBag.CityId = new SelectList(cities, "Id", "Value", null);
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
                db.AccountReferences.Add(new AccountReference { UserId = db.Users.Where(a => a.Login == User.Identity.Name).Select(a => a.Id).First(), PersonId = p.Id });
                db.SaveChanges();
                return RedirectToAction("CreateStep2", new { personIdFrom = p.Id });
            }

            InitSelectLists();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectSavedPerson(int? SavedPerson)
        {
            if (SavedPerson != null)
            {
                return RedirectToAction("CreateStep2", new { personIdFrom = SavedPerson });
            }
            TempData["alertMessage"] = "Отправитель не выбран.";
            return View(); // 777 прописать ошибку и исправить представление
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
                Package package = new Package { PersonIdFrom = fromId, PersonIdTo = toId };
                db.Packages.Add(package);
                db.SaveChanges();
                db.UserPackages.Add(new UserPackage { PackageId = package.Id, UserId = db.Users.Where(a => a.Login == User.Identity.Name).Select(a => a.Id).First() });
                db.Records.Add(new Record { PackageId = package.Id, DateAndTime = DateTime.Now, Status = Statuses[0] });
                db.SaveChanges();
                TempData["alertMessage"] = "Отправление успешно зарегистрировано. При принятии груза работником службы доставки в накладную будут внесены остальные данные об отправлении.";
                return RedirectToAction("Details", new { id = package.Id });
            }

            InitSelectLists();
            return View(temp);
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
